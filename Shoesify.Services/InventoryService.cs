using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
        private readonly IValidator<UpdateInventoryRequest> _updateValid;
        private readonly IValidator<DisableInventoryRequest> _disableValid;
        public InventoryService(ShoesifyContext context, JwtTokenService jwtTokenService, IValidator<CreateInventoryRequest> validator, IValidator<UpdateInventoryRequest> updateValid, IValidator<DisableInventoryRequest> disableValid)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
            _validator = validator;
            _updateValid = updateValid;
            _disableValid = disableValid;
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

        public async Task<List<Inventory>> ReadAllInventory()
        {
            return await _context.Inventories
                .Include(i => i.Stocks) 
                .Include(i => i.Imports) 
                    .ThenInclude(im => im.ImportDetails) 
                .Include(i => i.Exports) 
                    .ThenInclude(ex => ex.ExportDetails) 
                .ToListAsync();
        }

        public async Task<Inventory> GetAnInventory(string id)
        {
            return await _context.Inventories
                .Include(i => i.Stocks)
                .Include(i => i.Imports)
                    .ThenInclude(im => im.ImportDetails)
                .Include(i => i.Exports)
                    .ThenInclude(ex => ex.ExportDetails)
                .FirstOrDefaultAsync(i => i.InventoryId == id);
        }

        public async Task<bool> UpdateInventory(UpdateInventoryRequest updatedInventory)
        {
            var validate = await _updateValid.ValidateAsync(updatedInventory);

            if (!validate.IsValid)
            {
                var errorMessages = string.Join("; ", validate.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errorMessages}");
            }
            (string userId, string role) = _jwtTokenService.GetIdAndRoleFromToken();
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.InventoryId == updatedInventory.InventoryId);

            if (inventory == null)
            {
                return false;
            }

            inventory.Name = updatedInventory.Name;
            inventory.Location = updatedInventory.Location;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DisableInventory(DisableInventoryRequest inventory)
        {
            var validate2 = await _disableValid.ValidateAsync(inventory);

            if (!validate2.IsValid)
            {
                var errorMessages = string.Join("; ", validate2.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errorMessages}");
            }
            (string userId, string role) = _jwtTokenService.GetIdAndRoleFromToken();

            var invenDisable = await _context.Inventories
                .FirstOrDefaultAsync(i => i.InventoryId == inventory.InventoryId);

            if (invenDisable == null)
            {
                return false;
            }

            invenDisable.Status = false;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
