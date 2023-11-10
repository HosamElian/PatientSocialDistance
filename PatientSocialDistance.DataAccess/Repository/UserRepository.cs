using Microsoft.EntityFrameworkCore;
using PatientSocialDistance.DataAccess.Data;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Models;

namespace PatientSocialDistance.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _context.ApplicationUsers.AsQueryable().ToListAsync();
            
        }

        public async Task<IEnumerable<UserFroSearchDTO>> GetbyUsernameAsync(string username)
        {
            return await _context.ApplicationUsers.AsQueryable()
                .Where(x=> x.UserName.Contains(username))
                .Select(u => new UserFroSearchDTO { Name =  u.UserName })
                .ToListAsync();
        }
    }
}
