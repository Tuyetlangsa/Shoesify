using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoesify.Services.Requests
{
    public class ImportDetailRequest
    {
        public string ShoeDetailId { get; set; }
        public int? Quantity { get; set; }
    }
}
