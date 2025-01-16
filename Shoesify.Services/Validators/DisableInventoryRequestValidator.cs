using FluentValidation;
using Shoesify.Services.Requests;

namespace Shoesify.Services.Validators
{
    public class DisableInventoryRequestValidator : AbstractValidator<DisableInventoryRequest>
    {
        public DisableInventoryRequestValidator()
        {
            RuleFor(v => v.InventoryId).NotNull().NotEmpty().MaximumLength(10);
        }
    }
}
