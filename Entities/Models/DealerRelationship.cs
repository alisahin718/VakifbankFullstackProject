using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class DealerRelationship : BaseModel
    {
        public int DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }
        public int PriceListId { get; set; }
        public virtual PriceList PriceList { get; set; }
        public decimal Discount { get; set; }
    }
}
