namespace PatientSocialDistance.Persistence.DTOs
{
    public class VisitApprovalDto
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int DurationInMinutes { get; set; }
        public bool IsStartDateChange { get; set; }
        public string NewDate { get; set; }
    }
}
