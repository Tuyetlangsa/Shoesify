using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shoesify.Services.Requests;

namespace Shoesify.Services.Validators
{
    public class GetAllExportRequestValidator : AbstractValidator<GetAllExportRequest>
    {
        public GetAllExportRequestValidator()
        {
            RuleFor(v => v.inventoryId).NotNull().NotEmpty().MaximumLength(10);
        }
    }
}
