﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shoesify.Services.Responses
{
    public class ImportDetailResponse
    {
        
        public string ShoeDetailId { get; set; }
        public int? Quantity { get; set; }
    }
}
