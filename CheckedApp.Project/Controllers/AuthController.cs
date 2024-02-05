using CheckedAppProject.API.Contracts;
using CheckedAppProject.LOGIC.DTOs;
using CheckedAppProject.LOGIC.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CheckedAppProject.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authenticationService;

        public AuthController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            var result = await _authenticationService.RegisterAsync(request.addUserDto);

            if (!result.Success)
            {
                AddErrors(result);
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName));
        }

        private void AddErrors(AuthResult result)
        {
            foreach (var error in result.ErrorMessages)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            var result = await _authenticationService.LoginAsync(request.Email, request.Password);

            if (!result.Success)
            {
                AddErrors(result);
                return BadRequest(ModelState);
            }

            return Ok(new AuthResponse(result.Email, result.UserName, result.Token, result.RefreshToken));
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] RefreshTokenDTO refreshToken)
        {
            var result = await _authenticationService.RefreshTokenAsync(refreshToken);

            if (!result.Success)
            {
                AddErrors(result);
                return BadRequest(ModelState);
            }

            return Ok(new AuthResponse(result.Email, result.UserName, result.Token, result.RefreshToken));
        }

    }

}
