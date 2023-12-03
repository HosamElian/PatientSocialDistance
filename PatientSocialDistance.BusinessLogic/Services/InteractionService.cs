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
        private readonly IVisitService _visitService;

        public InteractionService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IVisitService visitService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _visitService = visitService;
        }
        public Result Add(string[] names)
        {
            Vist? visit = null;
            names.ToList().ForEach(name =>
          {
              var check = _visitService.HasVisit(name);
              if (check != null)
              {
                  visit = _visitService.HasVisit(name);
              }
          });
            if (visit == null) return new Result();

            DateTime interactonDate = visit.StartVistDate;
            var user = _userManager.FindByIdAsync(visit.VistedUserId).Result;
            var vistor = _userManager.FindByIdAsync(visit.VistorUserId).Result;



            if (user == null || vistor == null) return new Result();

            var interaction = new Interaction()
            {
                InteractionDate = interactonDate,
                DurationInMinutes = visit.DurationInMinutes,
                UserId = user.Id,
                VistorUserId = vistor.Id,
                VistId = visit.Id,
                IsDeleted = false,

            };
            _unitOfWork.InteractionRepository.Add(interaction);

            if (_unitOfWork.Save().Result)
            {
                return new Result()
                {
                    IsCompleted = true,
                    Message = ResultMessages.ProcessCompleted,
                };
            }
            return new Result();
        }

        public async Task<Result> GetAllAsync(string username)
        {
            var user = _userManager.FindByNameAsync(username).Result;
            if (user == null) return new Result();
            var interactions = await _unitOfWork.InteractionRepository.GetAllAsync(user.Id);
            var interactionsGrouppedById = interactions.GroupBy(x => x.VistorUserId).Select(x => new 
            {
                x.First().VistorUser.Name,
                Time = x.Sum(u=> u.DurationInMinutes),

            }).ToList();
            List<InteractionForUserDto> interactionList = interactionsGrouppedById.Select(x => new InteractionForUserDto
            {
                Name = x.Name,
                Time = x.Time
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

        public Result returnDuration(string[] names)
        {
            Vist visit = new();
            names.ToList().ForEach(name =>
            {
                var check = _visitService.HasVisit(name);
                if (check != null)
                {
                    visit = _visitService.HasVisit(name);
                }
            });
            if (visit == null) return new Result();
            return new Result()
            {
                IsCompleted = true,
                Message = ResultMessages.ProcessCompleted,
                Value = visit.DurationInMinutes.ToString()
            };

        }
    }
}
