using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly TokenService _tokenService;

        public AuthController(ILogger<AuthController> logger, TokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] UserViewModel request)
        {
            string token;
            if (_tokenService.IsAuthenticated(request, out token))
            {
                return Ok(new { token = token });
            }
            else
            {
                return BadRequest("Usuário ou senha invalido(s)!");
            }

        }
    }
}