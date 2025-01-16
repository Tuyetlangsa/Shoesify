using Shoesify.Entities.Models;
using Shoesify.Services.Requests;
using Shoesify.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoesify.Services.Abstractions
{
    public interface IImportService
    {
        public Task<int> CreateImport(ImportRequest importRequest);
        public Task<Import> GetImportByID(string id);

        public Task<List<ImportResponse>> GetAllImportOfInventory(GetAllImportRequest request);
    }
}
