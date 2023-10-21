using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.NotDbModels;

namespace PatientSocialDistance.BusinessLogic.Services.IServices
{
    public interface IVisitService
    {
        Result CreateVisit(VisitDto visitDto);
        Task<Result> VisitApproval(VisitApprovalDto visitApproval);
        Task<Result> GetAllVisits(string userId);
        Task<Result> GetVisitsByDate(string userId, DateOnly date);
    }
}
