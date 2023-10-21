using PatientSocialDistance.Persistence.NotDbModels;

namespace PatientSocialDistance.BusinessLogic.Services.IServices
{
    public interface IAuthService
    {
        Task<AuthModelResponse> RegisterAsync(RegisterModel registerModel);
        Task<AuthModelResponse> GetTokenAsync(TokenRequestModel tokenRequestModel);
        Task<string> AddRoleAsync(AddRoleModel addRoleModel);
    }
}
