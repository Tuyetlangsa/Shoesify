﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shoesify.Services.Requests;

namespace Shoesify.Services.Validators
{
    public  class GetAllImportRequestValidator : AbstractValidator<GetAllImportRequest>
    {
        public GetAllImportRequestValidator()
        {
            RuleFor(v => v.inventoryId).NotNull().NotEmpty().MaximumLength(10);
        }
    }
}
