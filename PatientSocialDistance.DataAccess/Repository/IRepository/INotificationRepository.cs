using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.DataAccess.Repository.IRepository
{
    public interface INotificationRepository
    {
        Notification GetById(int id);
        void AddNotification(Notification notification);
        Task<IEnumerable<NotificationDTO>> GetNotificationsOfTargetUserAsync(string userId);
    }
}
