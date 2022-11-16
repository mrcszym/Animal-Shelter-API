using AnimalShelter.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Data.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly JwtAuthManager jwtAuthManager;
        public LoginController(JwtAuthManager jwtAuthManager)
        {
            this.jwtAuthManager = jwtAuthManager;
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authorize([FromBody] User usr)
        {
            var token = jwtAuthManager.Authenticate(usr.username, usr.password);
            if (string.IsNullOrEmpty(token))
                return Unauthorized();
            return Ok(token);
        }
    }
}
