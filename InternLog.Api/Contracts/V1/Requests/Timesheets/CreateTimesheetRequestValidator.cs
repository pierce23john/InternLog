using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternLog.Api.Contracts.V1.Requests.Timesheets
{
    public class CreateTimesheetRequestValidator : AbstractValidator<CreateTimesheetRequest>
    {
        public CreateTimesheetRequestValidator()
        {
            RuleFor(timesheet => timesheet.TimeIn)
                .LessThan(timesheet => timesheet.TimeOut)
                .WithMessage("Time in must be earlier than time out.");
        }
    }
}
