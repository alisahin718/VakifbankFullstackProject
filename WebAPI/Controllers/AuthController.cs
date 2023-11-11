using Business.Auth;
using Castle.Core.Smtp;
using Core.Utilities.Jwt;
using Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenHandler _tokenHadler;
        private readonly IEmailSender _emailSender;

        public AuthController(IAuthService authService, ITokenHandler tokenHadler, IEmailSender emailSender)
        {
            _authService = authService;
            _tokenHadler = tokenHadler;
            _emailSender = emailSender;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromForm] RegisterAuthDto authDto)
        {
            var result = await _authService.Register(authDto);
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UserLogin(LoginAuthDto authDto)
        {
            var result = await _authService.UserLogin(authDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DealerLogin(DealerLoginDto dealerLoginDto)
        {
            var result = await _authService.DealerLogin(dealerLoginDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
