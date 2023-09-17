using PatientSocialDistance.Persistence.Models;

namespace PatientSocialDistance.DataAccess.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();

    }
}
