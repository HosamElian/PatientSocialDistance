using Microsoft.AspNetCore.Identity;
using PatientSocialDistance.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.DataAccess.Repository.IRepository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<string>> GetAllAsync();

    }
}
