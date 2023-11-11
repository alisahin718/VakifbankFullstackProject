using Core.Utilities.Jwt;
using Core.Utilities.Result.Abstract;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Auth
{
    public interface IAuthService
    {
        Task<IResult> Register(RegisterAuthDto registerDto);
        Task<IDataResult<AdminToken>> UserLogin(LoginAuthDto loginDto);
        Task<IDataResult<DealerToken>> DealerLogin(DealerLoginDto dealerLoginDto);
    }
}
