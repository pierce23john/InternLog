using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternLog.Api.Services.Contracts
{
    public interface ILinkGeneratorService
    {
        Task<string> GenerateConfirmationEmailLink(Guid userId, string token);
    }
}
