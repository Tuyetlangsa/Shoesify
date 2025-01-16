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
    public class CreateImportValidation : AbstractValidator<ImportRequest>
    {
        public CreateImportValidation() 
        {
            RuleFor(i => i.ImportID).NotNull().NotEmpty().MaximumLength(10);
            RuleFor(i => i.SupplierID).NotNull().NotEmpty().MaximumLength(10);
            RuleFor(i => i.InventoryID).NotNull().NotEmpty().MaximumLength(10);
           
        }
    }
}
