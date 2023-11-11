using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class OrderDto : Order
    {
        public string DealerName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
