using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Models;

namespace PatientSocialDistance.DataAccess.Repository.IRepository
{
    public interface IVistRepository
    {

        Task<IEnumerable<VisitorRequestVisitDTO>> GetAllAsync(string userId, bool isApproved);
        Task<Vist> GetByIdAsync(int Id);
        Task<IEnumerable<VisitsAcceptedDTO>> GetByIdAndDateAsync(string UserId, string username, DateOnly date, bool? approved = true);
        Task<bool> IsTheresVisitInSameTime(DateTime date);
        void Add(Vist vist);
        Vist hasVisit(string userId); 

    }
}
