using FluentValidation;
using Shoesify.Services.Requests;

namespace Shoesify.Services.Validators
{
    public class UpdateInventoryRequestValidator : AbstractValidator<UpdateInventoryRequest>
    {
        public UpdateInventoryRequestValidator() 
        {
            RuleFor(v => v.InventoryId).NotNull().NotEmpty().MaximumLength(10);
            RuleFor(v => v.Name).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(v => v.Location).NotNull().NotEmpty().MaximumLength(30);
        }       
    }
}
