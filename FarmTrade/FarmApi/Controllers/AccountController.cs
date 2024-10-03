using FarmBusiness.Services;
using Microsoft.AspNetCore.Http;
using FarmTradeEntity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace FarmApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserService _userService;
        public AccountController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] User user)
        {
            #region Edit User:
            _userService.UpdateUser(user);
            return Ok("User Details updated successfully");
            #endregion
        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody] User user)
        {
            #region Register
            _userService.AddUser(user);
            return Ok("Register successfully!!");
            #endregion
        }

        [NonAction] 
        public string GenerateToken(string email, string role)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }

            var claims = new[]
            {
              new Claim(ClaimTypes.Name, email),
              new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKeyWithAtLeast128Bits"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:44329",
                audience: "https://localhost:44329",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {
            // Ensure the user email and role are provided
            if (string.IsNullOrWhiteSpace(user.email) || string.IsNullOrWhiteSpace(user.role))
            {
                return BadRequest(new { message = "Email and role are required to generate a token.", code = -1 });
            }

            // Generate the token
            var token = GenerateToken(user.email, user.role);

            // Return the token
            return Ok(new { token });
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] User user)
        {

            #region Login
            var result = _userService.Login(user);
            // Check for valid roles
            if (result == "admin" || result == "supplier")
            {
                var token = GenerateToken(user.email, result);
                return Ok(new { message = "Login successful", code = 1, role = result, token }); // Return role
            }
            else if (result == "Invalid")
            {
                return BadRequest(new { message = "Login failed", code = -1 });
            }
            else
            {
                return StatusCode(500, new { message = "An error occurred.", code = 0 });
            }
            #endregion
        }
        
        [HttpGet("GetUserById")]
        public User GetUserById(int userId)
        {
            #region Get User By Id
            return _userService.GetUserById(userId);
            #endregion
        }
        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            #region Get User:
            return _userService.GetUsers();
            #endregion
        }
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int userId)
        {
            #region Delete User
            _userService.DeleteUser(userId);
            return Ok("User deleted successfully!!!");
            #endregion
        }
    }
}
