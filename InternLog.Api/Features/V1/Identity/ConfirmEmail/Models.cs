using FastEndpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternLog.Api.Features.V1.Identity.ConfirmEmail
{
    public class ConfirmEmailRequest
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }

    public class ConfirmEmailRequestValidator : Validator<ConfirmEmailRequest>
    {
        public ConfirmEmailRequestValidator()
        {

        }
    }

    public class ConfirmEmailSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class ConfirmEmailFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
