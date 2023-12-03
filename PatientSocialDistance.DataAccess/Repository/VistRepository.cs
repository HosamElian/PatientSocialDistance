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

        public async Task<IEnumerable<VisitorRequestVisitDTO>> GetAllAsync(string UserId, bool isApproved)
        {
            var query = _context.Vists.AsQueryable();
            return await query.Include(vv=> vv.VistorUser).Where(x => x.VistedUserId == UserId && x.Approved == isApproved && x.VistStatusId == (int)VistApprovalStatusEnum.Requested).Select(v=> new VisitorRequestVisitDTO
            {
                VisitId = v.Id,
                VisitDate = v.StartVistDate.ToString("dd/MM/yyyy"),
                VisitMessage = v.Message,
                VisitorName = v.VistorUser.Name,
                IsStartDateChange = false,
                DurationInMinutes = 30,
                NewDate = String.Empty,
            }).ToListAsync();
        }

        public async Task<IEnumerable<VisitsAcceptedDTO>> GetByIdAndDateAsync(string UserId, string username, DateOnly date, bool? approved = true)
        {
            var query = _context.Vists.AsQueryable();
            return await query
                            .Where(x => x.VistedUserId == UserId
                                     && x.Approved == approved
                                     && x.StartVistDate.Year  == date.Year
                                     && x.StartVistDate.Month == date.Month
                                     && x.StartVistDate.Day == date.Day)
                            .Include(x => x.VistorUser)
                            .Select(v => new VisitsAcceptedDTO
                            {
                                StartDate = v.StartVistDate.ToString("dd/MM/yyyy"),
                                VisitedUsername = username,
                                VisitorUsername = v.VistorUser.Name,
                                Message = v.Message,
                                StartTime = TimeOnly.FromDateTime(v.StartVistDate).ToString(),
                                DurationInMinutes = v.DurationInMinutes
                            })
                            .ToListAsync();
        }

        public Task<Vist> GetByIdAsync(int Id)
        {
            return _context.Vists.Include(u => u.VistorUser).Include(u=> u.VistedUser).FirstOrDefaultAsync(v => v.Id == Id);
        }

        public Vist hasVisit(string userId)
        {
            return _context.Vists
                .Include(u => u.VistorUser)
                .Include(u => u.VistedUser)
                .Where(v => v.VistorUserId == userId && v.StartVistDate <= DateTime.Now && v.Approved).FirstOrDefault();
        }

        public async Task<bool> IsTheresVisitInSameTime(DateTime date)
        {
            var sameDate = await _context.Vists.Where(v => (v.StartVistDate.Year == date.Year &&
                                                    v.StartVistDate.Month == date.Month &&
                                                    v.StartVistDate.Day == date.Day)).ToListAsync();
            var result = sameDate.Where(sm => (sm.StartVistDate.Hour == date.Hour) ||
                                               (sm.StartVistDate.AddMinutes(sm.DurationInMinutes).Hour == date.Hour));
            return result.Any();
        }
    }
}
