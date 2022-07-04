using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Data.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.ViewModel.Account;

namespace Knowledge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        private readonly IAccountService _accountService;

        public AuthenticateController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel data)
        {
            var identity = await _accountService.GetIdentity(data);
            var user = await _accountService.GetIdentity(data);
            if (user == null)
            {
                return BadRequest("Неправельный логин или пароль");
            }

            var tokenString = _accountService.GenerateJSONWebToken(identity);
            var response = Ok(new { Token = tokenString });
            
            return response;
        }
        
    }

}
 
