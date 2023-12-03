using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBlockRepository BlockRepository { get; }
        IInteractionRepository InteractionRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IVistRepository VistRepository { get; }
        INotificationRepository NotificationRepository { get; }

        Task<bool> Save();
    }
}
