using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternLog.Api.Contracts.V1.Responses.Identity
{
    public class LoginFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
