using PatientSocialDistance.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.DataAccess.Repository.IRepository
{
    public interface IVistRepository
    {
        
        Task<IEnumerable<Vist>> GetAllAsync(string UserId);
        Task<Vist> GetByIdAsync(int Id);
        Task<IEnumerable<Vist>> GetByIdAndDateAsync(string UserId, DateOnly date, bool? approved = false);
        void Add(Vist vist);

    }
}
