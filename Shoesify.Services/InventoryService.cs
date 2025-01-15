using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shoesify.Entities.Models;
using Shoesify.Services.Abstractions;
using Shoesify.Services.Requests;

namespace Shoesify.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly ShoesifyContext _context;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IValidator<CreateInventoryRequest> _validator;

        public InventoryService(ShoesifyContext context, JwtTokenService jwtTokenService, IValidator<CreateInventoryRequest> validator)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
            _validator = validator;
        }
        
        public async Task<int> CreateAnInventory(CreateInventoryRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errorMessages}");
            }
            
            (string userId, string role) = _jwtTokenService.GetIdAndRoleFromToken();
            Inventory inv = new Inventory()
            {
                UserId = userId,
                InventoryId = request.InventoryId,
                Name = request.Name,
                Location = request.Location,
            };
             _context.Inventories.Add(inv);
             return await _context.SaveChangesAsync();
        }
    }
}
