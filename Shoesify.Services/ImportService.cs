using Azure.Core;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shoesify.Entities.Models;
using Shoesify.Services.Abstractions;
using Shoesify.Services.Requests;
using Shoesify.Services.Responses;
using Shoesify.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoesify.Services
{

    public class ImportService : IImportService
    {
        private readonly ShoesifyContext _context;
        private IValidator<ImportRequest> _importValidate;
        private IValidator<ImportDetailRequest> _importDetailValidate;

        public ImportService(ShoesifyContext context, IValidator<ImportDetailRequest> importDetailValidate, IValidator<ImportRequest> validator)
        {
            _context = context;
            _importDetailValidate = importDetailValidate;
            _importValidate = validator;
        }
        public async Task<int> CreateImport(ImportRequest importRequest)
        {
           
            var importValid = await _importValidate.ValidateAsync(importRequest);

            if (!importValid.IsValid)
            {
                var errorMessages = string.Join("; ", importValid.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errorMessages}");
            }
            var import = new Import();
            import.ImportId = importRequest.ImportID;
            import.SupplierId = importRequest.SupplierID;
            import.ImportDate = importRequest.ImportDate;
            
            _context.Imports.Add(import);
            
            foreach (var detail in importRequest.ImportDetails)
            {
                var importDetailValid = await _importDetailValidate.ValidateAsync(detail);

                if (!importDetailValid.IsValid)
                {
                    var errorMessages = string.Join("; ", importDetailValid.Errors.Select(e => e.ErrorMessage));
                    throw new ValidationException($"Validation failed: {errorMessages}");
                }
                var importDetail = new ImportDetail();
                importDetail.ImportId = importRequest.ImportID;
                importDetail.ShoeDetailId = detail.ShoeDetailId;
                importDetail.Quantity = detail.Quantity;
                _context.ImportDetails.Add(importDetail);

                var stock = await _context.Stocks.FirstOrDefaultAsync(i => i.ShoeDetailId == detail.ShoeDetailId);
                if (stock != null)
                {
                    stock.Quantity += detail.Quantity;
                }
                else
                {
                     
                        _context.Stocks.Add(new Stock
                        {
                            ShoeDetailId = detail.ShoeDetailId,
                            InventoryId = importRequest.InventoryID, 
                            Quantity = detail.Quantity
                        });
                    
                }
                
            }
            return await _context.SaveChangesAsync();


        }

       

        public async Task<Import> GetImportByID(string id)
        {
            if (id == null) { throw new ArgumentException(nameof(id), "Invalid id"); }
            var import = await _context.Imports.Include(i => i.ImportDetails).FirstOrDefaultAsync(i => i.ImportId == id);

            if (import == null)
            {
                throw new KeyNotFoundException($"No import found with ID: {id}");
            }




          
            return import;

        }
    }
}
