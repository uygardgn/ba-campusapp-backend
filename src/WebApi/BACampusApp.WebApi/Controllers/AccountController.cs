using BACampusApp.Authentication.Options;
using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Concretes;
using BACampusApp.Business.Constants;
using BACampusApp.Core.Utilities.Results;
using BACampusApp.Dtos.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using NuGet.Common;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Security.Claims;
using System.Text;

namespace BACampusApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IAccountService _accountService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IEmailService _emailService;
        private readonly ITokenBlackListService _tokenBlackListService;

        public AccountController(IJwtService jwtService, IAccountService accountService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, JwtOptions jwtOptions, IEmailService emailService, ITokenBlackListService tokenBlackListService)
        {
            _jwtService = jwtService;
            _accountService = accountService;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions;
            _emailService = emailService;
            _tokenBlackListService = tokenBlackListService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return NotFound(Messages.UserNotFound);

            var isValidPassword = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!isValidPassword.Succeeded)
                return Unauthorized(Messages.UsernameOrPasswordIsWrong);

            string token = await _jwtService.GenerateTokenAsync(user);
            return Ok(new AuthResult
            {
                IsSuccess = true,
                Token = token,
                Message = "Token oluşturma başarılı"
            });
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //Token için verilen süreyi sıfırlar
            _jwtOptions.ExpiredTime = TimeSpan.Zero;
            //Mevcut token alınır
            var currentToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            //TokenBlackList e kaydedilir
            var result = await _tokenBlackListService.CreateAsync(currentToken);

            if (result)
            {
                return Ok(new AuthResult
                {
                    IsSuccess = true,
                    Token = null,
                    Message = "Çıkış işlemi başarılı."
                });
            }
            return BadRequest();
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminRole + "," + Roles.TrainerRole + "," + Roles.StudentRole)]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _accountService.ChangePasswordAsync(changePasswordDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var tokenBytes = Encoding.UTF8.GetBytes(token);
                var base64Token = WebEncoders.Base64UrlEncode(tokenBytes);
                var reactAppUrl = "http://localhost:3000";
                var passwordResetLink = $"{reactAppUrl}/ResetPassword?email={forgotPasswordDto.Email}&token={base64Token}";
                var message = new string[] { user.Email, "Forgot Password Link", passwordResetLink };

                _emailService.SendResetEmailLinkAsync(forgotPasswordDto.Email, passwordResetLink);

                return Ok(new AuthResult
                {
                    IsSuccess = true,
                    Message = $"Password change request is sent on Email {user.Email}. Please open your email and click the link",
                    Token = passwordResetLink
                });
            }
            else
            {
                return NotFound(Messages.UserNotFound);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPasswordDto { Token = token, Email = email };
            return Ok(new
            {
                model
            });
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var tokenBytes = WebEncoders.Base64UrlDecode(resetPassword.Token);
                var originalToken = Encoding.UTF8.GetString(tokenBytes);
                var resetPassResult = await _userManager.ResetPasswordAsync(user, originalToken, resetPassword.newPassword);
                if (!resetPassResult.Succeeded)
                {
                    return BadRequest(Messages.ResetPasswordFail);
                }

                return Ok(new AuthResult
                {
                    IsSuccess = true,
                    Message = $"Password hass been changed"
                });
            }
            else
            {
                return NotFound(Messages.UserNotFound);
            }
        }

    }
}
