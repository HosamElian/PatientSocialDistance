using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public InteractionService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public Result Add(InteractionDto interactionDto)
        {
            DateTime interactonDate = DateTime.Parse(interactionDto.InteractionDate);
            var user = _userManager.FindByNameAsync(interactionDto.Username).Result;
            var vistor = _userManager.FindByNameAsync(interactionDto.VistorName).Result;
            if (user == null || vistor == null) return new Result();

            var interaction = new Interaction()
            {
                InteractionDate = interactonDate,
                DurationInMinutes = interactionDto.DurationInMinutes,
                UserId = user.Id,
                VistorUserId = vistor.Id,
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

        public async Task<Result> GetAllAsync(string username)
        {
            var user = _userManager.FindByNameAsync(username).Result;
            if(user == null) return new Result();
            var interactions = await _unitOfWork.InteractionRepository.GetAllAsync(user.Id);
            List<InteractionForUserDto> interactionList = interactions.Select(x => new InteractionForUserDto
            {
                Name = x.VistorUser.Name,
                Time = x.DurationInMinutes
            }).ToList();

            if (interactions.Any())
            {
                return new Result()
                {
                    IsCompleted = true,
                    Message = ResultMessages.ProcessCompleted,
                    Value = interactionList,
                };
            }
            return new Result();
        }
    }
}
