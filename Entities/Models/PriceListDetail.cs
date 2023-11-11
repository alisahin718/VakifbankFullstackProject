using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PriceListDetail : BaseModel
    {
        public int PriceListId { get; set; }
        public virtual PriceList PriceList { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal Price { get; set; }
    }
}
