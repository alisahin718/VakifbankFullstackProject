using Entities.Models;

namespace Core.Utilities.Jwt
{
    public interface ITokenHandler
    {
        AdminToken CreateUserToken(User user, List<OperationClaim> operationClaims);
        DealerToken CreateDealerToken(Dealer dealer);
    }
}
