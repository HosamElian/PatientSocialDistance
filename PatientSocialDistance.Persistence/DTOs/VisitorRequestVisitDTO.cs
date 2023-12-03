namespace PatientSocialDistance.Persistence.DTOs
{
    public class VisitorRequestVisitDTO
    {
        public int VisitId { get; set; }
        public string VisitorName { get; set; }
        public string VisitDate { get; set; }
        public string VisitMessage { get; set; }
        public int DurationInMinutes { get; set; }
        public bool IsStartDateChange { get; set; }
        public string NewDate { get; set; }
    }
}
