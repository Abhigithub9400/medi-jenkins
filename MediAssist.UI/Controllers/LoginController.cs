using MediAssist.Application.Abstract.Services;
using MediAssist.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MediAssist.UI.Controllers
{
    [Route("api/manage")]
    [ApiController]
    public class LoginController : Controller
    {
        #region PRIVATE FIELDS
        private readonly IFirebaseAuthService _authService;
        #endregion

        #region CONSTRUCTOR
        public LoginController(IFirebaseAuthService authService)
        {
            _authService = authService;
        }
        #endregion

        #region PUBLIC METHODS
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var response = await _authService.SignInWithEmailAndPasswordAsync(loginViewModel.Email, loginViewModel.Password);

            if (response == HttpStatusCode.OK || response == HttpStatusCode.Created)
            {
                return Ok(new { success = true, redirectUrl = "/home" });
            }

            return BadRequest(new { success = false, message = "Invalid login attempt." });
        }

        [HttpGet("api/login/google-signin")]
        public IActionResult SignInWithGoogle()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Login");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            var googleLoginUrl = Url.Content($"~/signin-google?redirect_uri={Uri.EscapeDataString(redirectUrl)}");

            return Ok(new { redirectUrl = googleLoginUrl });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            var claims = result.Principal.Identities
                .FirstOrDefault()?.Claims
                .Select(claim => new
                {
                    claim.Type,
                    claim.Value
                });

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region PRIVATE METHODS
        #endregion
    }
}
