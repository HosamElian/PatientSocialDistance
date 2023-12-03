using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Models;
using PatientSocialDistance.Persistence.NotDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.BusinessLogic.Services.IServices
{
    public interface IInteractionService
    {
        Result Add(string[] names);
        Result returnDuration(string[] names);
        Task<Result> GetAllAsync(string username);
    }
}
