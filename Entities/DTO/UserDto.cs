﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string? NewPassword { get; set; }
    }
}
