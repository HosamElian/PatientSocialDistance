using Microsoft.AspNetCore.Identity;
using PatientSocialDistance.Persistence.NotDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.BusinessLogic.Services.IServices
{
    public interface IUserService
    {
        Task<Result> GetUsers(string username);
    }
}
