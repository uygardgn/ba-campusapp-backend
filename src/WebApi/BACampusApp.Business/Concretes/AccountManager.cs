
using BACampusApp.Business.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace BACampusApp.Business.Concretes
{
    public class AccountManager : IAccountService
    {
        private readonly IEmailService _emailManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtService _jwtManager;

        public AccountManager(IEmailService emailManager, UserManager<IdentityUser> userManager, IStringLocalizer<Resource> stringLocalizer, SignInManager<IdentityUser> signInManager, IJwtService jwtManager)
        {
            _emailManager = emailManager;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _signInManager = signInManager;
            _jwtManager = jwtManager;
        }

        public async Task<IResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            string email = changePasswordDto.Email;
            string newPassword = changePasswordDto.NewPassword;
            string oldPassword = changePasswordDto.OldPassword;

            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser == null)
                return new ErrorResult(_stringLocalizer[Messages.UserNotFound]);

            var changePasswordResult = await _userManager.ChangePasswordAsync(identityUser, oldPassword, newPassword);
            if (!changePasswordResult.Succeeded)
                return new ErrorResult(_stringLocalizer[Messages.UsernameOrPasswordIsWrong]);

            return new SuccessResult(_stringLocalizer[Messages.PasswordChangedSuccessfully]);
        }
                
        public async Task<IResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            string email = resetPasswordDto.Email;
            string newPassword = AuthenticationHelper.GenerateRandomPassword();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new ErrorResult(_stringLocalizer[Messages.UserNotFound]);

            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, passwordResetToken, newPassword);

            if (!resetPasswordResult.Succeeded)
                return new ErrorResult(_stringLocalizer[Messages.IncorrectOperation]);
            
            await _emailManager.SendEmailAsync(email, newPassword);

            return new SuccessResult(_stringLocalizer[Messages.PasswordResetSuccessfully]);
        }

        public async Task<AuthResult> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return new AuthResult
                {
                    IsSuccess = false,
                    Token = null,
                    Message = _stringLocalizer[Messages.UserNotFound]
                };

            var isValidPassword = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!isValidPassword.Succeeded)
                return new AuthResult
                {
                    IsSuccess = false,
                    Token = null,
                    Message = _stringLocalizer[Messages.UsernameOrPasswordIsWrong]
                };

            string token = await _jwtManager.GenerateTokenAsync(user);
            return new AuthResult
            {
                IsSuccess = true,
                Token = token,
                Message = _stringLocalizer[Messages.TokenCreatedSuccessfully]
            };
        }
    }
}