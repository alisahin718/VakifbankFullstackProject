﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User : BaseModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Password { get; set; }
    }
}
