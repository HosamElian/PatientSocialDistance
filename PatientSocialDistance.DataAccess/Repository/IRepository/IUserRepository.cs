using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Models;

namespace PatientSocialDistance.DataAccess.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<IEnumerable<UserFroSearchDTO>> GetbyUsernameAsync(string username);

    }
}
