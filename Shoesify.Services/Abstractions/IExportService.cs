using Shoesify.Services.Requests;
using Shoesify.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoesify.Services.Abstractions
{
    public interface IExportService
    {
        public Task<int> CreateExport(CreateExportRequest request);
        public Task<ExportResponse> ReadExport(string exportId);

        public Task<List<ExportResponse>> GetAllExportOfInventory(GetAllExportRequest request);
    }
}
