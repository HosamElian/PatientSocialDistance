using Microsoft.EntityFrameworkCore.Query.Internal;
using PatientSocialDistance.Persistence.Models;

namespace PatientSocialDistance.DataAccess.Repository.IRepository
{
    public interface IBlockRepository
    {
        Task<Block> GetByUserIdIdAsync(string UserId, string UserBlockedId);
        Task<IEnumerable<Block>> GetAllAsync(string userID);
        
        void Add(Block block);


    }
}
