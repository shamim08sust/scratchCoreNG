
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScratchApp.API.Data;
using ScratchApp.API.Models;

namespace ScratchApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        public AuthController(IAuthRepository repository)
        {
            _repository = repository;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            username = username.ToLower();
            if (await _repository.UserExist(username))
                return BadRequest("Username Already Exist"); 

            var userToCreate = new User
            {
                Username = username
            };
            var createdUser = await _repository.Register(userToCreate, password);

            return StatusCode(201);
        }
    }
}