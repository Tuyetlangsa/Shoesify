using Shoesify.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoesify.Services.Requests
{
    public class ImportRequest
    {
        public string ImportID { get; set; }
        public string SupplierID { get; set; }
        public string InventoryID { get; set; }
        public DateOnly? ImportDate { get; set; }
        public List<ImportDetailRequest> ImportDetails { get; set; }
    }
    
}
