﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public int Stock { get; set; }
    }
}
