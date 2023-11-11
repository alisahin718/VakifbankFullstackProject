using Core.DataAccess;
using DataAccess.Context;
using DataAccess.Repositories.Contract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EFCore
{
    public class OperationClaimRepository : RepositoryBase<OperationClaim>, IOperationClaimRepository
    {
        public OperationClaimRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
