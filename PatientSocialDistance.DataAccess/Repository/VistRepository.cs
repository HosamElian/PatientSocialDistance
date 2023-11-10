using Microsoft.EntityFrameworkCore;
using PatientSocialDistance.DataAccess.Data;
using PatientSocialDistance.DataAccess.Repository.IRepository;
using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Enum;
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

        public async Task<IEnumerable<VisitorDto>> GetAllAsync(string UserId, bool isApproved)
        {
            var query = _context.Vists.AsQueryable();
            return await query.Include(vv=> vv.VistorUser).Where(x => x.VistedUserId == UserId && x.Approved == isApproved && x.VistStatusId == (int)VistApprovalStatusEnum.Requested).Select(v=> new VisitorDto
            {
                VisitId = v.Id,
                VisitDate = v.VistDate.ToString("dd/MM/yyyy"),
                VisitMessage = v.Message,
                VisitorName = v.VistorUser.Name,
            }).ToListAsync();
        }

        public async Task<IEnumerable<Vist>> GetByIdAndDateAsync(string UserId, DateOnly date, bool? approved = true)
        {
            var query = _context.Vists.AsQueryable();
            return await query
                            .Where(x => x.VistedUserId == UserId
                                     && x.Approved == approved
                                     && x.VistDate.Year  == date.Year
                                     && x.VistDate.Month == date.Month
                                     && x.VistDate.Day == date.Day)
                            .Include(x => x.VistorUser)
                            .ToListAsync();
        }

        public Task<Vist> GetByIdAsync(int Id)
        {
            return _context.Vists.FirstOrDefaultAsync(v => v.Id == Id);
        }
    }
}
