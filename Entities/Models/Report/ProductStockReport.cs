using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Report
{
    public class ProductStockReport : BaseModel
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string ProductName { get; set; }
        public int StockQuantity { get; set; }
    }
}
