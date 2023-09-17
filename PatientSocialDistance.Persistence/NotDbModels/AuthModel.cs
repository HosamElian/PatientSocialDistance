namespace PatientSocialDistance.Persistence.NotDbModels
{
    public class AuthModel
    {
        public bool IsAuthenticated { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
