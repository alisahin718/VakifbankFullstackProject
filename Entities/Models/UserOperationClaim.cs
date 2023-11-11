using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserOperationClaim : BaseModel
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int OperationClaimId { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
    }
}
