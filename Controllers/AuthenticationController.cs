using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using qwerty_chat_api.DTOs;
using qwerty_chat_api.Models;
using qwerty_chat_api.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace qwerty_chat_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UsersService _usersService;
        private IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthenticationController(IConfiguration config, IMapper mapper, UsersService usersService) 
        { 
            _config = config;
            _mapper = mapper;
            _usersService = usersService;
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(LogRequest logRequest)
        {
            var res = await Authenticate(logRequest);
            return res.Success ? Ok(res) : BadRequest(res);
        }


        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register(LogRequest logRequest)
        {
            try
            {
                await _usersService.CreateAsync(_mapper.Map<LogRequest, User>(logRequest));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        

        private async Task<LogResponse> Authenticate(LogRequest logRequest)
        {
            var user = await _usersService.GetAsync(logRequest.Username, logRequest.Password);

            if (user == null)
            {
                return new LogResponse()
                {
                    Success = false,
                    AccessToken = "Invalid username or password",
                };
            }
            return new LogResponse()
            {
                Success = true,
                AccessToken = GenerateJWTToken(user),
            };
        }

        private string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
