namespace PatientSocialDistance.Persistence.DTOs
{
    public class BlockUserDto
    {
        public string UserMakeBlockId { get; set; }
        public string UserBlockedId { get; set; }
        public bool MakeBlock { get; set; }
    }
}
