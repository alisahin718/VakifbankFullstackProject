using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Report
{
    public class AdminOrderReport : BaseModel
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime ReportDate { get; set; }
        public int DailyOrderCount { get; set; }
        public int WeeklyOrderCount { get; set; }
        public int MonthlyOrderCount { get; set; }
    }
}
