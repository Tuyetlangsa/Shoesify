using Shoesify.Entities.Models;
using Shoesify.Services.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoesify.Services.Responses
{
    public class ImportResponse
    {
        public string ImportID { get; set; }
        public string SupplierID { get; set; }
        public string InventoryID { get; set; }
        public DateOnly? ImportDate { get; set; }
        public List<ImportDetailResponse> ImportDetails { get; set; }
    }
}
