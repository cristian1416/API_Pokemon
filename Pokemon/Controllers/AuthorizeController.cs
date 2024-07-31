using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Domain.Entities;
using Pokemon.Domain.Interfaces;

namespace Pokemon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorize _authorize;

        public AuthorizeController(IAuthorize authorize)
        {
            _authorize = authorize;
        }

        [HttpPost("Login")]
        public async Task <ActionResult> Login(AuthLogin authLogin)
        {
            bool result = await _authorize.ValidateUser(authLogin);

            if (result)
            {
                var token = await _authorize.TokenJWT();
                return Ok(new { token = "Bearer " + token });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
