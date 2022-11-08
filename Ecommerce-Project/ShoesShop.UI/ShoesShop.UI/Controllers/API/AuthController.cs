using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoesShop.DTO;
using ShoesShop.Service;
using ShoesShop.UI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoesShop.UI.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration configuration;
        private readonly IAdminService adminService;

        public AuthController(IConfiguration configuration, IAdminService adminService)
        {
            this.configuration = configuration;
            this.adminService = adminService;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginInfo)
        {
            loginInfo.Password = Functions.MD5Hash(loginInfo.Password);

            var admin = adminService.AuthenticateAdmin(loginInfo);
            if (admin != null)
            {
                var tokenStr = GenerateJSONWebToken(admin);

                return Ok(new { info = admin, token = tokenStr });
            }
            else
            {
                return BadRequest(); // bad request
            }
        }

        private string GenerateJSONWebToken(AdminViewModel admin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentitals = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                //new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                //new Claim(JwtRegisteredClaimNames.Name, userInfo.EmailAddress),
                //new Claim(ClaimTypes.Name, userInfo.UserName),
                //new Claim(ClaimTypes.Email, userInfo.EmailAddress),
                //new Claim(ClaimTypes.Role, "admin"),

                
                //new Claim("Name", userInfo.UserName),
                //new Claim("Email", userInfo.EmailAddress),
                //new Claim("Role", "employee"),

                new Claim(ClaimTypes.Name, admin.UserName),
                new Claim(ClaimTypes.Role, admin.RoleName),



                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120), // Thời điểm chỉ định hết hạn token
                signingCredentials: credentitals
                );

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        [Authorize(Roles = "Admin")] //  Thêm authorize vào phương thức muốn xác thực token
        [HttpPost("Post")]
        public List<string> Post()
        {
            // Code lấy thông tin từ token
            //var temp = User.Claims;

            //var role = User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Role).Value;

            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //IList<Claim> claims = identity.Claims.ToList();
            //var userName = claims[0].Value;

            return new List<string> { "Value1", "Value2", "Value3" };
        }

    }
}
