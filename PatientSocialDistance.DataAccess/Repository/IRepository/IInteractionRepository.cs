using PatientSocialDistance.Persistence.Models;

namespace PatientSocialDistance.DataAccess.Repository.IRepository
{
    public interface IInteractionRepository
    {
        Task<IEnumerable<Interaction>> GetAllAsync(string UserId);
        void Add(Interaction interaction);
    }
}
