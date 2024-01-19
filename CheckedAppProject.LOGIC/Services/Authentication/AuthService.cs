using CheckedAppProject.DATA.Entities;
using Microsoft.AspNetCore.Identity;

namespace CheckedAppProject.LOGIC.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> RegisterAsync(
            string email,
            string username,
            string password,
            int userAge,
            string userSurname,
            string userSex
            )

        {
            var result = await _userManager.CreateAsync(
                new AppUser
                {
                    UserName = username,
                    Email = email,
                    UserSurname = userSurname,
                    UserAge = userAge,
                    UserSex = userSex
                },
                password);

            if (!result.Succeeded)
            {
                return FailedRegistration(result, email, username, userSurname, userSex, userAge);
            }

            return new AuthResult(true, email, username, "");
        }

        private static AuthResult FailedRegistration(IdentityResult result, string email, string username, string userSurname, string userSex, int userAge)
        {
            var authResult = new AuthResult(false, email, username, "");

            foreach (var error in result.Errors)
            {
                authResult.ErrorMessages.Add(error.Code, error.Description);
            }

            return authResult;
        }
        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var managedUser = await _userManager.FindByEmailAsync(email);

            if (managedUser == null)
            {
                return InvalidEmail(email);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);
            if (!isPasswordValid)
            {
                return InvalidPassword(email, managedUser.UserName);
            }

            var accessToken = _tokenService.CreateToken(managedUser);

            return new AuthResult(true, managedUser.Email, managedUser.UserName, accessToken);
        }

        private static AuthResult InvalidEmail(string email)
        {
            var result = new AuthResult(false, email,"","");
            result.ErrorMessages.Add("Bad credentials", "Invalid email");
            return result;
        }

        private static AuthResult InvalidPassword(string email, string userName)
        {
            var result = new AuthResult(false, email, userName, "");
            result.ErrorMessages.Add("Bad credentials", "Invalid password");
            return result;
        }

    }

}
