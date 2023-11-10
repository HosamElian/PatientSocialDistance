using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.NotDbModels;

namespace PatientSocialDistance.BusinessLogic.Services.IServices
{
    public interface IVisitService
    {
        Result CreateVisit(VisitDto visitDto);
        Task<Result> VisitApproval(VisitApprovalDto visitApproval);
        Task<Result> GetAllVisits(string username, bool isApproved = true);
        Task<Result> GetVisitsByDate(string username, DateOnly date, bool isApproved = true);
    }
}
