using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class PriceListDetailDto : PriceListDetail
    {
        public string ProductName { get; set; }
    }
}
