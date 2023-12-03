using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.NotDbModels;

namespace PatientSocialDistance.BusinessLogic.Services.IServices
{
    public interface IBlockService
    {
        Task<bool> CheckBlockAsync(string userWantToCheck, string userTockeckIt);
        Task<Result> GetAllAsync(string username);

        Task<Result> CreateOrDeleteBlockAsync(BlockUserDTO blockUserDto);
    }
}
