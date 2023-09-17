using PatientSocialDistance.Persistence.NotDbModels;

namespace PatientSocialDistance.BusinessLogic.Services.IServices
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel registerModel);
        Task<AuthModel> GetTokenAsync(TokenRequestModel tokenRequestModel);
        Task<string> AddRoleAsync(AddRoleModel addRoleModel);
    }
}
