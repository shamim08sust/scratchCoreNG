
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScratchApp.API.Data;
using ScratchApp.API.dto;
using ScratchApp.API.Models;

namespace ScratchApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repository, IConfiguration config)
        {
            _config = config;
            _repository = repository;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegDto userForRegDto)
        {
            userForRegDto.username = userForRegDto.username.ToLower();
            if (await _repository.UserExist(userForRegDto.username))
                return BadRequest("Username Already Exist");

            var userToCreate = new User
            {
                Username = userForRegDto.username
            };
            var createdUser = await _repository.Register(userToCreate, userForRegDto.password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {

            throw new Exception("force exception");

            var userFromrepo = await _repository.Login(userForLoginDto.Username, userForLoginDto.Password);
            if (userFromrepo == null)
                return Unauthorized();


            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromrepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromrepo.Username),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8
             .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDecriptor);

            return Ok(new
            {
                token = tokenhandler.WriteToken(token)
            });

        }
    }
}