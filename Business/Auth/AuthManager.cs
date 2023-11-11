using Business.Repositories.Service;
using Core.Utilities.Business;
using Core.Utilities.Jwt;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Model;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Auth
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;
        private readonly IDealerService _dealerService;

        public AuthManager(IUserService userService, ITokenHandler tokenHandler, IDealerService dealerService)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
            _dealerService = dealerService;
        }

        public async Task<IDataResult<AdminToken>> UserLogin(LoginAuthDto loginDto)
        {
            var user = await _userService.GetByEmail(loginDto.Email);
            if (user == null)
            {
                return new ErrorDataResult<AdminToken>("Kullanıcı maili sistemde bulunamadı");
            }

            var result = loginDto.Password;
            List<OperationClaim> operationClaims = await _userService.GetUserOperationClaims(user.Id);
            if (result != null)
            {
                AdminToken token = new AdminToken();
                token = _tokenHandler.CreateUserToken(user, operationClaims);
                return new SuccessDataResult<AdminToken>(token);
            }
            return new ErrorDataResult<AdminToken>("Kullanıcı maili ya da şifre bilgisi yanlış");
        }

        public async Task<IDataResult<DealerToken>> DealerLogin(DealerLoginDto dealerLoginDto)
        {
            var dealer = await _dealerService.GetByEmail(dealerLoginDto.Email);
            if (dealer == null)
            {
                return new ErrorDataResult<DealerToken>("Kullanıcı maili sistemde bulunamadı");
            }
            var result = dealerLoginDto.Password;
            if (result != null)
            {
                DealerToken token = new DealerToken();
                token = _tokenHandler.CreateDealerToken(dealer);
                return new SuccessDataResult<DealerToken>(token);
            }
            return new ErrorDataResult<DealerToken>("Kullanıcı maili ya da şifre bilgisi yanlış");
        }


        public async Task<IResult> Register(RegisterAuthDto registerDto)
        {
            IResult result = BusinessRules.Run(
                await CheckIfEmailExists(registerDto.Email),
                CheckIfImageExtesionsAllow(registerDto.Image.FileName),
                CheckIfImageSizeIsLessThanOneMb(registerDto.Image.Length)
                );

            if (result != null)
            {
                return result;
            }

            await _userService.Add(registerDto);
            return new SuccessResult("Kullanıcı kaydı başarıyla tamamlandı");
        }

        private async Task<IResult> CheckIfEmailExists(string email)
        {
            var list = await _userService.GetByEmail(email);
            if (list != null)
            {
                return new ErrorResult("Bu mail adresi daha önce kullanılmış");
            }
            return new SuccessResult();
        }

        private IResult CheckIfImageSizeIsLessThanOneMb(long imgSize)
        {
            decimal imgMbSize = Convert.ToDecimal(imgSize * 0.000010);
            if (imgMbSize > 1)
            {
                return new ErrorResult("Yüklediğiniz resmi boyutu en fazla 1mb olmalıdır");
            }
            return new SuccessResult();
        }

        private IResult CheckIfImageExtesionsAllow(string fileName)
        {
            var ext = fileName.Substring(fileName.LastIndexOf('.'));
            var extension = ext.ToLower();
            List<string> AllowFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
            if (!AllowFileExtensions.Contains(extension))
            {
                return new ErrorResult("Eklediğiniz resim .jpg, .jpeg, .gif, .png türlerinden biri olmalıdır!");
            }
            return new SuccessResult();
        }
    }
}
