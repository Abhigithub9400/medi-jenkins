using MediAssist.Application.Abstract.Services;
using MediAssist.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MediAssist.UI.Controllers
{
    [Route("api/manage")]
    [ApiController]
    public class SingUpController : Controller
    {
        #region PRIVATE FIELD
        private readonly IFirebaseAuthService _authService;
        #endregion

        #region CONSTRUCTOR
        public SingUpController(IFirebaseAuthService authService)
        {
            _authService = authService;
        }
        #endregion

        #region PUBLIC METHODS
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            var response = await _authService.SignUpWithEmailAndPasswordAsync(signUpViewModel.Email, signUpViewModel.Password);

            if (response == HttpStatusCode.OK || response == HttpStatusCode.Created)
            {
                return Ok(new { success = true, redirectUrl = "/login" });
            }

            return BadRequest(new { success = false, message = "Invalid SignUp attempt." });
        }
        #endregion

        #region PRIVATE METHODS
        #endregion
    }
}
