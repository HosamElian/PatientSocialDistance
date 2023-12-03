using Microsoft.EntityFrameworkCore;
using PatientSocialDistance.DataAccess.Data;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.DataAccess.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
        }

        public Notification GetById(int id)
        {
            return _context.Notifications.FirstOrDefault(o => o.Id == id);
        }

        public async Task<IEnumerable<NotificationDTO>> GetNotificationsOfTargetUserAsync(string userId)
        {
            return await _context.Notifications.Where(u=> u.TargetUserId == userId && !u.IsDeleted).Include(u=> u.TargetUser).Include(u=> u.UserMakeAction).Select(n => new NotificationDTO
            {
                Id = n.Id,
                Message = n.Message,
                MakeActionUsername = n.UserMakeAction.UserName.ToLower(),
                TargetUsername = n.UserMakeAction.UserName.ToLower(),
                ChangeAccepted = n.ChangeAccepted,
                IsChangeDate = n.IsChangeDate,
                VisitId = n.VisitId,
            }).ToListAsync();
        }
    }
}
