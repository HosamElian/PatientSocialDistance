using Microsoft.EntityFrameworkCore;
using PatientSocialDistance.DataAccess.Data;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using PatientSocialDistance.Persistence.Models;

namespace PatientSocialDistance.DataAccess.Repository
{
    public class VistRepository : IVistRepository
    {
        private readonly ApplicationDbContext _context;

        public VistRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Vist vist)
        {
            _context.Vists.Add(vist);
        }

        public async Task<IEnumerable<Vist>> GetAllAsync(string UserId)
        {
            var query = _context.Vists.AsQueryable();
            return await query.Where(x => x.VistedUserId == UserId && !x.IsDeleted && x.Approved).ToListAsync();
        }

        public Task<Vist> GetByIdAsync(int Id)
        {
            return _context.Vists.FirstOrDefaultAsync(v => v.Id == Id);
        }
    }
}
