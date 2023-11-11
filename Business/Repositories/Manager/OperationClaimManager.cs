using Business.Repositories.Messages;
using Business.Repositories.Service;
using Core.Utilities.Business;
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
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimRepository _context;
        public OperationClaimManager(IOperationClaimRepository context)
        {
            _context = context;
        }

        
        public async Task<IResult> Add(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(await IsNameExistForAdd(operationClaim.Name));
            if (result != null)
            {
                return result;
            }

            await _context.Add(operationClaim);
            return new SuccessResult(OperationClaimMessages.Added);
        }

        public async Task<IResult> Update(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(await IsNameExistForUpdate(operationClaim));
            if (result != null)
            {
                return result;
            }

            await _context.Update(operationClaim);
            return new SuccessResult(OperationClaimMessages.Updated);
        }

        public async Task<IResult> Delete(OperationClaim operationClaim)
        {
            await _context.Delete(operationClaim);
            return new SuccessResult(OperationClaimMessages.Deleted);
        }

        public async Task<IDataResult<List<OperationClaim>>> GetList()
        {
            return new SuccessDataResult<List<OperationClaim>>(await _context.GetAll());
        }

        public async Task<IDataResult<OperationClaim>> GetById(int id)
        {
            var result = await _context.Get(p => p.Id == id);
            return new SuccessDataResult<OperationClaim>(result);
        }

        public async Task<OperationClaim> GetByIdForUserService(int id)
        {
            var result = await _context.Get(p => p.Id == id);
            return result;
        }

        private async Task<IResult> IsNameExistForAdd(string name)
        {
            var result = await _context.Get(p => p.Name == name);
            if (result != null)
            {
                return new ErrorResult(OperationClaimMessages.NameIsNotAvaible);
            }
            return new SuccessResult();
        }

        private async Task<IResult> IsNameExistForUpdate(OperationClaim operationClaim)
        {
            var currentOperationClaim = await _context.Get(p => p.Id == operationClaim.Id);
            if (currentOperationClaim.Name != operationClaim.Name)
            {
                var result = await _context.Get(p => p.Name == operationClaim.Name);
                if (result != null)
                {
                    return new ErrorResult(OperationClaimMessages.NameIsNotAvaible);
                }
            }
            return new SuccessResult();
        }
    }
}
