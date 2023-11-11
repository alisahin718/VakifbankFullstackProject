using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class DealerDto : Dealer
    {
        public int? PriceListId { get; set; }
        public string? PriceListName { get; set; }
        public decimal? Discount { get; set; }
    }
}
