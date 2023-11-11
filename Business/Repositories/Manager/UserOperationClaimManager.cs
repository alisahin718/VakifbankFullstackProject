using Business.Repositories.Messages;
using Business.Repositories.Service;
using Core.Utilities.Business;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Model;
using DataAccess.Repositories.Contract;
using Entities.Models;

namespace Business.Repositories.Manager
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserService _userService;

        public UserOperationClaimManager(IUserOperationClaimRepository userOperationClaimRepository, 
            IOperationClaimService operationClaimService, 
            IUserService userService)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _operationClaimService = operationClaimService;
            _userService = userService;
        }


        public async Task<IResult> Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimRepository.Delete(userOperationClaim);
            return new SuccessResult(UserOperationClaimMessages.Deleted);
        }

        public async Task<IDataResult<UserOperationClaim>> GetById(int id)
        {
            return new SuccessDataResult<UserOperationClaim>(await _userOperationClaimRepository.Get(p => p.Id == id));
        }

        public async Task<IDataResult<List<UserOperationClaim>>> GetList()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(await _userOperationClaimRepository.GetAll());
        }

        public async Task<IResult> Update(UserOperationClaim userOperationClaim)
        {
            IResult result = BusinessRules.Run(
                await IsUserExist(userOperationClaim.UserId),
                await IsOperationClaimExist(userOperationClaim.OperationClaimId),
                await IsOperationSetExistForUpdate(userOperationClaim)
                );
            if (result != null)
            {
                return result;
            }

            await _userOperationClaimRepository.Update(userOperationClaim);
            return new SuccessResult(UserOperationClaimMessages.Updated);
        }

        public async Task<IResult> Add(UserOperationClaim userOperationClaim)
        {
            IResult result = BusinessRules.Run(
                await IsUserExist(userOperationClaim.UserId),
                await IsOperationClaimExist(userOperationClaim.OperationClaimId),
                await IsOperationSetExistForAdd(userOperationClaim)
                );
            if (result != null)
            {
                return result;
            }

            await _userOperationClaimRepository.Add(userOperationClaim);
            return new SuccessResult(UserOperationClaimMessages.Added);
        }

        public async Task<IResult> IsUserExist(int userId)
        {
            var result = await _userService.GetByIdForAuth(userId);
            if (result == null)
            {
                return new ErrorResult(UserOperationClaimMessages.UserNotExist);
            }
            return new SuccessResult();
        }

        public async Task<IResult> IsOperationClaimExist(int operationClaimId)
        {
            var result = await _operationClaimService.GetByIdForUserService(operationClaimId);
            if (result == null)
            {
                return new ErrorResult(UserOperationClaimMessages.OperationClaimNotExist);
            }
            return new SuccessResult();
        }

        public async Task<IResult> IsOperationSetExistForAdd(UserOperationClaim userOperationClaim)
        {
            var result = await _userOperationClaimRepository.Get(p => p.UserId == userOperationClaim.UserId && p.OperationClaimId == userOperationClaim.OperationClaimId);
            if (result != null)
            {
                return new ErrorResult(UserOperationClaimMessages.OperationClaimSetExist);
            }
            return new SuccessResult();
        }

        private async Task<IResult> IsOperationSetExistForUpdate(UserOperationClaim userOperationClaim)
        {
            var currentUserOperationClaim = await _userOperationClaimRepository.Get(p => p.Id == userOperationClaim.Id);
            if (currentUserOperationClaim.UserId != userOperationClaim.UserId || currentUserOperationClaim.OperationClaimId != userOperationClaim.OperationClaimId)
            {
                var result = await _userOperationClaimRepository.Get(p => p.UserId == userOperationClaim.UserId && p.OperationClaimId == userOperationClaim.OperationClaimId);
                if (result != null)
                {
                    return new ErrorResult(UserOperationClaimMessages.OperationClaimSetExist);
                }
            }
            return new SuccessResult();
        }
    }
}
