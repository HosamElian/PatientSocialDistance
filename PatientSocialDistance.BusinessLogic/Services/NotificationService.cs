using Microsoft.AspNetCore.Identity;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using PatientSocialDistance.Persistence;
using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Models;
using PatientSocialDistance.Persistence.NotDbModels;

namespace PatientSocialDistance.BusinessLogic.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<Result> AddNotification(NotificationDTO notificationDTO)
        {
            var targetUser = await _userManager.FindByNameAsync(notificationDTO.TargetUsername);
            var MakeActionUser = await _userManager.FindByNameAsync(notificationDTO.MakeActionUsername);

            if (targetUser is not null || MakeActionUser is not null) { }

            var notifications = new Notification
            {
                Message = notificationDTO.Message,
                ChangeAccepted = notificationDTO.ChangeAccepted,
                IsChangeDate = notificationDTO.IsChangeDate,
                VisitId = notificationDTO.VisitId,
                TargetUserId = targetUser.Id,
                UserMakeActionId = MakeActionUser.Id
            };
            _unitOfWork.NotificationRepository.AddNotification(notifications);
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
        public async Task AddNotificationNoSave(NotificationDTO notificationDTO)
        {
            var targetUser = await _userManager.FindByNameAsync(notificationDTO.TargetUsername);
            var MakeActionUser = await _userManager.FindByNameAsync(notificationDTO.MakeActionUsername);

            if (targetUser is not null || MakeActionUser is not null) { }

            var notifications = new Notification
            {
                Message = notificationDTO.Message,
                ChangeAccepted = notificationDTO.ChangeAccepted,
                IsChangeDate = notificationDTO.IsChangeDate,
                VisitId = notificationDTO.VisitId,
                TargetUserId = targetUser.Id,
                UserMakeActionId = MakeActionUser.Id
            };
            _unitOfWork.NotificationRepository.AddNotification(notifications);
        }

        public async Task<Result> ChangeDateResultAsync(ChangeRequestDTO request)
        {
            var visit = await _unitOfWork.VistRepository.GetByIdAsync(request.VisitId);
            var vistor = _userManager.FindByIdAsync(visit.VistorUserId).Result;
            var visited = _userManager.FindByIdAsync(visit.VistedUserId).Result;


            if (visit is null) return new Result();

            var notification = _unitOfWork.NotificationRepository.GetById(request.NotificationId);
            visit.Approved = false;
            visit.IsDeleted = false;
            notification.IsDeleted = true;

            await AddNotificationNoSave(new NotificationDTO()
            {
                ChangeAccepted = false,
                IsChangeDate = true,
                VisitId = request.VisitId,
                MakeActionUsername = vistor.UserName,
                TargetUsername = visited.UserName,
                Message = $"Your Change in date has been refused by {vistor.Name}"
            });

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

        public async Task<Result> GetAllNotificationsforUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var result = await _unitOfWork.NotificationRepository.GetNotificationsOfTargetUserAsync(user.Id);

            if (user is not null)
            {
                return new Result()
                {
                    IsCompleted = true,
                    Message = ResultMessages.ProcessCompleted,
                    Value = result
                };
            }
            return new Result();
        }
    }
}
