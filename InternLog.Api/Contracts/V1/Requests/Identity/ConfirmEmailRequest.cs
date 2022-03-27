namespace InternLog.Api.Contracts.V1.Requests.Identity
{
    public class ConfirmEmailRequest
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}
