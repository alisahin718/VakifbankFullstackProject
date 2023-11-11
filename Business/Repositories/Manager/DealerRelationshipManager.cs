using Business.Repositories.Messages;
using Business.Repositories.Service;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Model;
using DataAccess.Repositories.Contract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.Manager
{
    public class DealerRelationshipManager : IDealerRelationshipService
    {
        private readonly IDealerRelationshipRepository _context;

        public DealerRelationshipManager(IDealerRelationshipRepository context)
        {
            _context = context;
        }

        public async Task<IResult> Add(DealerRelationship dealerRelationship)
        {
            await _context.Add(dealerRelationship);
            return new SuccessResult(DealerRelationshipMessages.Added);
        }

        public async Task<IResult> Update(DealerRelationship dealerRelationship)
        {
            var result = await _context.Get(p => p.DealerId == dealerRelationship.DealerId);
            if (result != null)
            {
                dealerRelationship.Id = result.Id;
                await _context.Update(dealerRelationship);
            }
            else
            {
                await _context.Add(dealerRelationship);
            }

            return new SuccessResult(DealerRelationshipMessages.Updated);
        }

        public async Task<IResult> Delete(DealerRelationship dealerRelationship)
        {
            await _context.Delete(dealerRelationship);
            return new SuccessResult(DealerRelationshipMessages.Deleted);
        }

        public async Task<IDataResult<List<DealerRelationship>>> GetList()
        {
            return new SuccessDataResult<List<DealerRelationship>>(await _context.GetAll());
        }

        public async Task<IDataResult<DealerRelationship>> GetById(int id)
        {
            return new SuccessDataResult<DealerRelationship>(await _context.Get(p => p.Id == id));
        }

        public async Task<IDataResult<DealerRelationship>> GetByDealerId(int dealerId)
        {
            return new SuccessDataResult<DealerRelationship>(await _context.Get(p => p.DealerId == dealerId));
        }

    }
}
