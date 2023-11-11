using Core.DataAccess;
using DataAccess.Context;
using DataAccess.Repositories.Contract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EFCore
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public readonly RepositoryContext _context;

        public UserRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<OperationClaim>> GetUserOperatinonClaims(int userId)
        {
            var result = from userOperationClaim in _context.UserOperationClaims.Where(p => p.UserId == userId)
                         join operationClaim in _context.OperationClaims on userOperationClaim.OperationClaimId equals operationClaim.Id
                         select new OperationClaim
                         {
                             Id = operationClaim.Id,
                             Name = operationClaim.Name
                         };
            return await result.OrderBy(p => p.Name).ToListAsync();
        }
    }
}
