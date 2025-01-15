using Microsoft.EntityFrameworkCore;
using Shoesify.Entities.Models;
using Shoesify.Services.Requests;
using Shoesify.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoesify.Services
{
    public class ExportService
    {
        private readonly ShoesifyContext _context;
        private readonly JwtTokenService _jwtTokenService;

        public ExportService(ShoesifyContext context, JwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }
        public async Task<int> CreateExport(CreateExportRequest request)
        {          
            (string userId, string role) = _jwtTokenService.GetIdAndRoleFromToken();
   

            var export = new Export
            {
                InventoryId = request.InventoryId,
                ExportDate = request.ExportDate
            };

            _context.Exports.Add(export);
            await _context.SaveChangesAsync(); 

       
            foreach (var detail in request.Details)
            {
              
                var stock = _context.Stocks
                    .FirstOrDefault(s => s.InventoryId == request.InventoryId && s.ShoeDetailId == detail.ShoeDetailId);

                if (stock == null || stock.Quantity < detail.Quantity)
                {
                    throw new InvalidOperationException($"Not enough stock for ShoeDetailId: {detail.ShoeDetailId}");
                }

                var exportDetail = new ExportDetail
                {
                    ExportId = export.ExportId,
                    ShoeDetailId = detail.ShoeDetailId,
                    Quantity = detail.Quantity
                };

                stock.Quantity -= detail.Quantity;
                _context.ExportDetails.Add(exportDetail);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<ExportResponse> ReadExport(string exportId)
        {
            var exportEntity = await _context.Exports
                .Where(e => e.ExportId == exportId)
                .Include(e => e.ExportDetails) 
                    .ThenInclude(d => d.ShoeDetail) 
                    .ThenInclude(sd => sd.Shoe) 
                .FirstOrDefaultAsync();

            if (exportEntity == null)
            {
                throw new KeyNotFoundException($"Export with ID {exportId} not found.");
            }

            var exportResponse = new ExportResponse(
                exportEntity.ExportId,
                exportEntity.InventoryId ?? string.Empty,
                exportEntity.ExportDate ?? DateOnly.MinValue,
                exportEntity.ExportDetails.Select(d => new ExportDetailResponse(
                    d.ShoeDetailId,
                    d.Quantity ?? 0, 
                    d.ShoeDetail.Shoe.Name, 
                    d.ShoeDetail.Shoe.Brand,
                    d.ShoeDetail.Size ?? 0,
                    d.ShoeDetail.Color 
                )).ToList()
            );

            return exportResponse;
        }


    }
}
