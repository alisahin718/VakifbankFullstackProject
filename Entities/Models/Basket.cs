using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Basket : BaseModel
    {
        public int DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }  
    }
}
