﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class ProductListDto : Product
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> Images { get; set; }
    }
}
