using Microsoft.AspNetCore.Identity;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using PatientSocialDistance.Persistence;
using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Enum;
using PatientSocialDistance.Persistence.Models;
using PatientSocialDistance.Persistence.NotDbModels;
using System;
using System.Globalization;

namespace PatientSocialDistance.BusinessLogic.Services
{
    public class VisitService : IVisitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlockService _blockService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notificationService;

        public VisitService(IUnitOfWork unitOfWork, IBlockService blockService,
            UserManager<ApplicationUser> userManager,
            INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _blockService = blockService;
            _userManager = userManager;
            _notificationService = notificationService;
        }



        public async Task<Result> GetAllVisits(GetVisitRequest request)
        {
            var user = _userManager.FindByNameAsync(request.Username).Result;
            if (user == null) return new Result();

            var result = await _unitOfWork.VistRepository.GetAllAsync(user.Id, request.IsApproved);
            if (result.Any())
            {
                return new Result
                {
                    IsCompleted = true,
                    Message = ResultMessages.ProcessCompleted,
                    Value = result
                };
            }
            return new Result() { Message = ResultMessages.NoVisitExist };
        }

        public async Task<Result> GetVisitsByDate(GetVisitByDateRequest request)
        {
            var user = _userManager.FindByNameAsync(request.Username).Result;
            if (user == null) return new Result();

            DateOnly visitDate = DateOnly.FromDateTime(DateTime.Parse(request.Date));

            var visits = await _unitOfWork.VistRepository.GetByIdAndDateAsync(user.Id, user.UserName, visitDate, request.IsApproved);


            if (visits.Any()) return new Result() { Value = visits, IsCompleted = true, Message = ResultMessages.ProcessCompleted };

            return new Result() { Message = ResultMessages.NoVisitExist };
        }

        public async Task<Result> CreateVisit(VisitDto visitDto)
        {
            if (_blockService.CheckBlockAsync(visitDto.VisitedUsername, visitDto.VisitorUsername).Result) return new Result() { Value = 403 };
            DateTime visitDate = DateTime.Now;
            try
            {
                var timeOnly = TimeOnly.Parse(visitDto.VisitTime);
                var dateOnly = DateOnly.Parse(visitDto.VisitDate);
                visitDate = new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day, timeOnly.Hour, timeOnly.Minute, timeOnly.Second);
            }
            catch (Exception ex) { int x = 1; }
            if (await _unitOfWork.VistRepository.IsTheresVisitInSameTime(visitDate)) return new Result() { Value = 400 };
            var visitorUser = _userManager.FindByNameAsync(visitDto.VisitorUsername).Result;
            var visitedUser = _userManager.FindByNameAsync(visitDto.VisitedUsername).Result;
            if (null == visitorUser || null == visitedUser) return new Result();


            Vist visit = new()
            {
                IsDeleted = false,
                VistedUserId = visitedUser.Id,
                Approved = false,
                Reason = "",
                Message = visitDto.Message,
                StartVistDate = visitDate,
                VistorUserId = visitorUser.Id,
                VistStatusId = (int)VistApprovalStatusEnum.Requested,
            };

            _unitOfWork.VistRepository.Add(visit);
            await _notificationService.AddNotificationNoSave(new NotificationDTO
            {
                VisitId = visit.Id,
                MakeActionUsername = visitDto.VisitorUsername,
                TargetUsername = visitDto.VisitedUsername,
                Message = $"{visitDto.VisitorUsername} request to see you at {visitDto.VisitDate}"
            });
            if (_unitOfWork.Save().Result)
            {
                return new Result
                {
                    IsCompleted = true,
                    Message = ResultMessages.ProcessCompleted,
                    Value = visitDto
                };
            }

            return new Result();
        }
        public async Task<Result> VisitApproval(VisitApprovalDto visitApproval)///
        {
            var visit = await _unitOfWork.VistRepository.GetByIdAsync(visitApproval.Id);

            if (visit == null || (visitApproval.StatusId != (int)VistApprovalStatusEnum.Approved && visitApproval.StatusId != (int)VistApprovalStatusEnum.Rejected))
                return new Result() { Message = ResultMessages.ValueOutOfRange };

            visit.VistStatusId = visitApproval.StatusId;

            if (visit.VistStatusId == (int)VistApprovalStatusEnum.Rejected) visit.Approved = false;

            if (visit.VistStatusId == (int)VistApprovalStatusEnum.Approved)
            {
                visit.Approved = true;
                visit.DurationInMinutes = visitApproval.DurationInMinutes;
                if (visitApproval.IsStartDateChange)
                {
                    DateTime oldDate = visit.StartVistDate;
                    TimeOnly timeOnly = TimeOnly.FromDateTime(visit.StartVistDate);
                    DateOnly dateOnly = DateOnly.Parse(visitApproval.NewDate);
                    visit.StartVistDate = new  DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day, timeOnly.Hour, timeOnly.Minute, timeOnly.Second);
                    visit.VistStatusId = (int)VistApprovalStatusEnum.WaitForRequesterToApprove;
                    var notification = new NotificationDTO
                    {
                        VisitId = visit.Id,
                        IsChangeDate = true,
                        MakeActionUsername = visit.VistedUser.UserName,
                        TargetUsername = visit.VistorUser.UserName,
                        Message = $"Visit Date Has been changed from {oldDate:dd/MM//yyyy} to {visit.StartVistDate:dd/MM//yyyy}",

                    };
                    await _notificationService.AddNotificationNoSave(notification);
                }
            }

            if (_unitOfWork.Save().Result)
            {
                return new Result
                {
                    IsCompleted = true,
                    Message = ResultMessages.ProcessCompleted,
                    Value = visitApproval
                };
            }

            return new Result();
        }

        public Vist? HasVisit(string name)
        {
            var user = _userManager.FindByNameAsync(name).Result;
            if (user != null)
            {
                return _unitOfWork.VistRepository.hasVisit(user.Id);

            }
            else return null;
        }
    }
}
