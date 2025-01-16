using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoesify.Services.Requests
{
    public sealed record CreateExportRequest(
     string InventoryId,
     DateOnly ExportDate,
     List<ExportDetailRequest> Details
 );

    public sealed record ExportDetailRequest(
        string ShoeDetailId,
        int Quantity
    );
}
