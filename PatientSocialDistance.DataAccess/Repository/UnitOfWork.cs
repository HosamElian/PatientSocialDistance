using PatientSocialDistance.DataAccess.Data;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            BlockRepository = new BlockRepository(context);
            InteractionRepository = new InteractionRepository(context);
            RoleRepository = new RoleRepository(context);
            UserRepository = new UserRepository(context);
            VistRepository = new VistRepository(context);
            NotificationRepository = new NotificationRepository(context);
            _context = context;
        }
        public IBlockRepository BlockRepository { get; private set; }

        public IInteractionRepository InteractionRepository { get; private set; }

        public IRoleRepository RoleRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IVistRepository VistRepository { get; private set; }

        public INotificationRepository NotificationRepository { get; private set; }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
