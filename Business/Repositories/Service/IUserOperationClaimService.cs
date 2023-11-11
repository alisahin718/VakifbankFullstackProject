using Core.Utilities.Result.Abstract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.Service
{
    public interface IUserOperationClaimService
    {
        Task<IResult> Add(UserOperationClaim userOperationClaim);
        Task<IResult> Update(UserOperationClaim userOperationClaim);
        Task<IResult> Delete(UserOperationClaim userOperationClaim);
        Task<IDataResult<List<UserOperationClaim>>> GetList();
        Task<IDataResult<UserOperationClaim>> GetById(int id);
    }
}
