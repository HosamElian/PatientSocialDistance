using Microsoft.EntityFrameworkCore;
using PatientSocialDistance.DataAccess.Data;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using PatientSocialDistance.Persistence.Models;

namespace PatientSocialDistance.DataAccess.Repository
{
    public class InteractionRepository : IInteractionRepository
    {
        private readonly ApplicationDbContext _context;

        public InteractionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Interaction interaction)
        {
            _context.Interactions.Add(interaction);
        }

        public async Task<IEnumerable<Interaction>> GetAllAsync(string UserId)
        {
            var query =  _context.Interactions.AsQueryable();
            return await query.Where(x => x.UserId == UserId).Include(x=> x.VistorUser).ToListAsync();
        }
    }
}
