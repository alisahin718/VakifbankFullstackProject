using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Report
{
    public class DealerOrderReport : BaseModel
    {
        public int DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }
        public string DealerName { get; set; }
        public List<Order> Orders { get; set; }
    }
}
