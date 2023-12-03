using PatientSocialDistance.Persistence.DTOs;
using PatientSocialDistance.Persistence.Models;
using PatientSocialDistance.Persistence.NotDbModels;

namespace PatientSocialDistance.BusinessLogic.Services.IServices
{
    public interface IVisitService
    {
        Task<Result> CreateVisit(VisitDto visitDto);
        Task<Result> VisitApproval(VisitApprovalDto visitApproval);
        Task<Result> GetAllVisits(GetVisitRequest getVisitRequest);
        Task<Result> GetVisitsByDate(GetVisitByDateRequest request);

        Vist? HasVisit(string name);
    }
}
