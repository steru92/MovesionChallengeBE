using Microsoft.AspNetCore.Mvc;
using MovesionChallengeWebApi.Helpers;
using MovesionChallengeWebApi.Models;
using MovesionChallengeWebApi.Services;

namespace MovesionChallengeWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(nameof(Authenticate))]
        public IActionResult Authenticate(AuthRequest authRequest)
        {
            var response = _authService.Authenticate(authRequest);

            if (response == null)
            {
                return BadRequest(new { message = Constants.Constants.ERROR_MESSAGE_WRONG_CREDENTIALS });
            }

            return Ok(response);
        }

        [Auth]
        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var users = _authService.GetAll();
            return Ok(users);
        }
    }
}