using Core.Utilities.Result.Abstract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.Service
{
    public interface IOperationClaimService
    {
        Task<IResult> Add(OperationClaim operationClaim);
        Task<IResult> Update(OperationClaim operationClaim);
        Task<IResult> Delete(OperationClaim operationClaim);
        Task<IDataResult<List<OperationClaim>>> GetList();
        Task<IDataResult<OperationClaim>> GetById(int id);
        Task<OperationClaim> GetByIdForUserService(int id);
    }
}
