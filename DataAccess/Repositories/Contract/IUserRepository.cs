using Core.DataAccess;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Contract
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<List<OperationClaim>> GetUserOperatinonClaims(int userId);
    }
}
