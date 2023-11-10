using Microsoft.AspNetCore.Identity;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using PatientSocialDistance.Persistence;
using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Enum;
using PatientSocialDistance.Persistence.Models;
using PatientSocialDistance.Persistence.NotDbModels;
using System.Globalization;

namespace PatientSocialDistance.BusinessLogic.Services
{
    public class VisitService : IVisitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlockService _blockService;
        private readonly UserManager<ApplicationUser> _userManager;

        public VisitService(IUnitOfWork unitOfWork, IBlockService blockService, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _blockService = blockService;
            _userManager = userManager;
        }
        


        public async Task<Result> GetAllVisits(string username, bool isApproved = true)
        {
            var user = _userManager.FindByNameAsync(username).Result;
            if (user == null) return new Result();

            var result  = await _unitOfWork.VistRepository.GetAllAsync(user.Id, isApproved);
            if(result.Any())
            {
                return new Result
                {
                    IsCompleted = true,
                    Message = ResultMessages.ProcessCompleted,
                    Value = result
                };
            }
            return new Result() {Message = ResultMessages.NoVisitExist };
        }

        public async Task<Result> GetVisitsByDate(string username, DateOnly date, bool isApproved = true)
        {
            var user = _userManager.FindByNameAsync(username).Result;
            if (user == null) return new Result();
            var visits = await _unitOfWork.VistRepository.GetByIdAndDateAsync(user.Id, date, isApproved);

            IEnumerable<VisitDto> visitorsList = visits.Select(v => new VisitDto
            {
                VisitDate = v.VistDate.ToString("dd/MM/yyyy"),
                VisitedUsername = user.Name,
                VisitorUsername = v.VistorUser.Name,
                Message = v.Message,
            });

            if (visitorsList.Any()) return new Result() {  Value = visitorsList, IsCompleted = true, Message = ResultMessages.ProcessCompleted };
            
            return new Result() { Message = ResultMessages.NoVisitExist};
        }

        public Result CreateVisit(VisitDto visitDto)
        {
            if (_blockService.CheckBlockAsync(visitDto.VisitedUsername, visitDto.VisitorUsername).Result) return new Result();

            var visitorUser = _userManager.FindByNameAsync(visitDto.VisitorUsername).Result;
            var visitedUser = _userManager.FindByNameAsync(visitDto.VisitedUsername).Result;
            if (null == visitorUser || null == visitedUser) return new Result();

            DateTime visitDate = DateTime.Parse(visitDto.VisitDate);

            Vist visit = new()
            {
                IsDeleted = false,
                VistedUserId = visitedUser.Id,
                Approved = false,
                Reason = "",
                Message = visitDto.Message,
                VistDate = visitDate,
                VistorUserId = visitorUser.Id,
                VistStatusId = (int)VistApprovalStatusEnum.Requested,
            };

            _unitOfWork.VistRepository.Add(visit);
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
        public async Task<Result> VisitApproval(VisitApprovalDto visitApproval)
        {
            var visit = await _unitOfWork.VistRepository.GetByIdAsync(visitApproval.Id);
            
            if (visit == null || (visitApproval.StatusId != (int)VistApprovalStatusEnum.Approved && visitApproval.StatusId != (int)VistApprovalStatusEnum.Rejected))
                return new Result() { Message = ResultMessages.ValueOutOfRange };

            visit.VistStatusId = visitApproval.StatusId;

            if (visitApproval.StatusId == (int)VistApprovalStatusEnum.Approved) visit.Approved = true;
            if (visitApproval.StatusId == (int)VistApprovalStatusEnum.Rejected) visit.Approved = false;

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


    }
}
