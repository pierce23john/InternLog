namespace InternLog.Api.Contracts.V1.Responses.Identity
{
    public class AuthenticationSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
