using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternLog.Api.Contracts.V1.Requests
{
    public class GetTimesheetRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
