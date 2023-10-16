using BKGestionTareas.DTO;
using BKGestionTareas.Models;
using BKGestionTareas.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BKGestionTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string secretKey;
        private ILoginRepository _loginRepository;

        public AutenticacionController(IConfiguration config, ILoginRepository loginRepository)
        {
            secretKey = config.GetSection("settings").GetSection("secretKey").ToString();
            _loginRepository = loginRepository;
        }

        [HttpPost]
        [Route("GenerarToken")]
        public IActionResult generateToken([FromBody] DtoUsuario request)
        {

            if (_loginRepository.VerifyUserExist(request))
            {
                string tokencreado = _loginRepository.CreateToken(request, secretKey);
                return StatusCode(StatusCodes.Status200OK, new { token = tokencreado });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }



        }
    }
}
