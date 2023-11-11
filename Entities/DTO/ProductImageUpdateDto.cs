using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class ProductImageUpdateDto : ProductImage
    {
        public IFormFile Image { get; set; }
    }
}
