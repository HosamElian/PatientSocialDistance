namespace PatientSocialDistance.Persistence.DTOs
{
    public class BlockUserDTO
    {
        public string UsernameMakeBlock { get; set; }
        public string UsernameBlocked { get; set; }
        public bool MakeBlock { get; set; }
        public bool HasNotification { get; set; }
    }
}
