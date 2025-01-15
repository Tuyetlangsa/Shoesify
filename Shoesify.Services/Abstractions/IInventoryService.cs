﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shoesify.Entities.Models;
using Shoesify.Services.Requests;

namespace Shoesify.Services.Abstractions
{
    public interface IInventoryService
    {
        public  Task<int> CreateAnInventory(CreateInventoryRequest inventory);
    }
}
