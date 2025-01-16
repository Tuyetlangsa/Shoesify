using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoesify.Services.Responses
{
    public sealed record ExportResponse(
        string ExportId,
        string InventoryId,
        DateOnly ExportDate,
        List<ExportDetailResponse> Details
    );

    public sealed record ExportDetailResponse(
        string ShoeDetailId,
        int Quantity,
        string ShoeName,
        string Brand,
        int Size,
        string Color
    );
}
