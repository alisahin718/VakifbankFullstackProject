using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Report
{
    public class LowStockReport : BaseModel
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string ProductName { get; set; }
        public int MinStockQuantity { get; set; } = 10;
        public int CurrentStockQuantity { get; set; }
        public string Description { get; set; }

        public LowStockReport()
        {
            if(CurrentStockQuantity < MinStockQuantity)
            {
                Description = ProductName + ": ürünün stoğu 10'un altına düşmüştür.";
            }
        }
    }
}
