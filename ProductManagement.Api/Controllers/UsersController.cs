using Microsoft.AspNetCore.Mvc;
using ProductManagement.Bl;
using ProductManagement.Models;

namespace ProductManagement.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Route("GetUsersByEmail/{email}")]
        public async Task<IActionResult> GetUser(string email)
        => Ok(await _usersService.GetUsersByEmailAsync(email));

        [HttpPost]
        [Route("CheckIn")]
        public async Task<IActionResult> CheckIn(UsersDto user)
        {
            var resultCheck = await _usersService.CheckIn(user.Username, user.Password);
            if(resultCheck == null) return NotFound();
            var forOutputClient = new UsersDto
            {
                Password = resultCheck.Password,
                Username = resultCheck.Username
            };
            return Ok(forOutputClient);
        }
       

    }
}
