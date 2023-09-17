using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using PatientSocialDistance.Persistence;
using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Models;
using PatientSocialDistance.Persistence.NotDbModels;

namespace PatientSocialDistance.BusinessLogic.Services
{
    public class InteractionService : IInteractionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InteractionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Result Add(InteractionDto interactionDto)
        {
            var interaction = new Interaction()
            {
                InteractionDate = interactionDto.InteractionDate,
                DurationInMinutes = interactionDto.DurationInMinutes,
                UserId = interactionDto.UserId,
                VistorUserId = interactionDto.VistorUserId,
                VistId = interactionDto.VistId,
                IsDeleted = false,

            };
            _unitOfWork.InteractionRepository.Add(interaction);

            if (_unitOfWork.Save().Result)
            {
                return new Result()
                {
                    IsCompleted = true,
                    Message = ResultMessages.ProcessCompleted,
                    Value = interactionDto,
                };
            }
            return new Result();
        }

        public async Task<Result> GetAllAsync(string UserId)
        {
            var interactions = await _unitOfWork.InteractionRepository.GetAllAsync(UserId);
            interactions = interactions.ToList();

            if (interactions.Count() > 0)
            {
                return new Result()
                {
                    IsCompleted = true,
                    Message = ResultMessages.ProcessCompleted,
                    Value = interactions,
                };
            }
            return new Result();
        }
    }
}
