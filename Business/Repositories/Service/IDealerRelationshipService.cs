using Core.Utilities.Result.Abstract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.Service
{
    public interface IDealerRelationshipService
    {
        Task<IResult> Add(DealerRelationship dealerRelationship);
        Task<IResult> Update(DealerRelationship dealerRelationship);
        Task<IResult> Delete(DealerRelationship dealerRelationship);
        Task<IDataResult<List<DealerRelationship>>> GetList();
        Task<IDataResult<DealerRelationship>> GetById(int id);
        Task<IDataResult<DealerRelationship>> GetByDealerId(int dealerId);
    }
}
