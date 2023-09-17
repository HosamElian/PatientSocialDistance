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
        void Add(Vist vist);

    }
}
