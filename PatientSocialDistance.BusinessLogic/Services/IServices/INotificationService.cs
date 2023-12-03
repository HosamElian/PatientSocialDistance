using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.NotDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.BusinessLogic.Services.IServices
{
    public interface INotificationService
    {
        Task<Result> AddNotification(NotificationDTO notificationDTO);
        Task AddNotificationNoSave(NotificationDTO notificationDTO);
        Task<Result> ChangeDateResultAsync(ChangeRequestDTO request);

        Task<Result> GetAllNotificationsforUserAsync(string username);


    }
}
