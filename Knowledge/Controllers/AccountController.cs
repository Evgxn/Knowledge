using Data.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.ViewModel.User;

namespace Knowledge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(AddUserViewModel vm)
        {
            await _userService.AddUserAsync(vm);
            return Ok();
        }
    }
}
