using Microsoft.AspNetCore.Identity;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using PatientSocialDistance.Persistence;
using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Enum;
using PatientSocialDistance.Persistence.Models;
using PatientSocialDistance.Persistence.NotDbModels;

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
        
        public Result CreateVisit(VisitDto visitDto)
        {
            if(_blockService.CheckBlockAsync(visitDto.VistedUserId, visitDto.VistorUserId).Result) return new Result();
            if(_userManager.FindByIdAsync(visitDto.VistedUserId).Result?.UserTypeId == (int)UserTypeEnum.Doctor) return new Result();

            Vist visit = new Vist
            {
                IsDeleted = false,
                VistedUserId = visitDto.VistedUserId,
                Approved = false,
                Reason = visitDto.Reason,
                Message = visitDto.Message,
                VistDate = visitDto.VistDate,
                VistorUserId = visitDto.VistorUserId,
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

        public async Task<Result> GetAllVisits(string userId)
        {
            var result  = await _unitOfWork.VistRepository.GetAllAsync(userId);
            if(result.Count() > 0)
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

        public async Task<Result> GetVisitsByDate(string userId, DateOnly date)
        {
            var visits = await _unitOfWork.VistRepository.GetByIdAndDateAsync(userId, date, true);

            IEnumerable<VisitorDto> visitorsList = visits.Select(v => new VisitorDto
            {
                VisitId = v.Id,
                VisitorName = v.VistorUser.Name,
                VisitorHospital = v.VistorUser.Hospital,
                VisitorPhoneNumber = v.VistorUser.PhoneNumber ?? "Not Available",
            });

            if (visitorsList.Any()) return new Result() {  Value = visitorsList, IsCompleted = true, Message = ResultMessages.ProcessCompleted };
            
            return new Result() { Message = ResultMessages.NoVisitExist};
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
