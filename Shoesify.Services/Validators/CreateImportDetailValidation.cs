using FluentValidation;
using Shoesify.Entities.Models;
using Shoesify.Services.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoesify.Services.Validators
{
    public class CreateImportDetailValidation : AbstractValidator<ImportDetailRequest>
    {
        public CreateImportDetailValidation()
        {
            RuleFor(i => i.ShoeDetailId).NotNull().NotEmpty().MaximumLength(10);
            
            RuleFor(i => i.Quantity).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
