using PatientSocialDistance.Persistence.DTOs;
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
        
        Task<IEnumerable<VisitorDto>> GetAllAsync(string userId, bool isApproved);
        Task<Vist> GetByIdAsync(int Id);
        Task<IEnumerable<Vist>> GetByIdAndDateAsync(string UserId, DateOnly date, bool? approved = true);
        void Add(Vist vist);

    }
}
