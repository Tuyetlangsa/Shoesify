using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shoesify.Entities.Models;
using Shoesify.Services.Abstractions;
using Shoesify.Services.Requests;

namespace Shoesify.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly ShoesifyContext _context;
        private readonly JwtTokenService _jwtTokenService;

        public InventoryService(ShoesifyContext context, JwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }
        
        public async Task<int> CreateAnInventory(CreateInventoryRequest request)
        {
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
