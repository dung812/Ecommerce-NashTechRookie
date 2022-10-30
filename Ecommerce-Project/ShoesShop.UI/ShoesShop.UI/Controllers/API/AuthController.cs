using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoesShop.DTO;
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

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //[HttpGet]
        //public IActionResult Login(string username, string pass)
        //{
        //    UserModel loginInfo = new UserModel();
        //    loginInfo.UserName = username;
        //    loginInfo.Password = pass;

        //    IActionResult response = Unauthorized();

        //    var user = AuthenticateUser(loginInfo);
        //    if (user != null)
        //    {
        //        var tokenStr = GenerateJSONWebToken(user);
        //        //response = Ok(new { token = tokenStr });


        //        return Ok(new { token = tokenStr });
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpPost]
        public IActionResult Login(UserModel loginInfo)
        {
            //IActionResult response = Unauthorized();

            var user = AuthenticateUser(loginInfo);
            if (user != null)
            {
                var tokenStr = GenerateJSONWebToken(user);
                //response = Ok(new { token = tokenStr });


                return Ok(new { token = tokenStr });
            }
            else
            {
                return NotFound();
            }
        }

        private UserModel AuthenticateUser(UserModel loginInfo)
        {
            UserModel user = null;
            if (loginInfo.UserName == "ntdung8124" && loginInfo.Password == "123")
            {
                user = new UserModel
                {
                    UserName = "ntdung8124",
                    Password = "123",
                    EmailAddress = "ntdung8124@gmail.com"
                };
            }
            return user;
        }

        private string GenerateJSONWebToken(UserModel userInfo)
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

                new Claim(ClaimTypes.Name, userInfo.UserName),
                new Claim(ClaimTypes.Email, userInfo.EmailAddress),
                new Claim(ClaimTypes.Role, "Admin"),

                //new Claim("Name", userInfo.UserName),
                //new Claim("Email", userInfo.EmailAddress),
                //new Claim("Role", "employee"),


                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120), // Thời điểm chỉ định hết hạn token, token sẽ hết hạn trể 5p
                signingCredentials: credentitals
                );

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        [Authorize(Roles = "Admin")] //  Theem authorize vào phương thức muốn xác thực token
        [HttpPost("Post")]
        public List<string> Post()
        {
            //var temp = User.Claims;

            //var role = User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Role).Value;

            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //IList<Claim> claims = identity.Claims.ToList();
            //var userName = claims[0].Value;

            return new List<string> { "Value1", "Value2", "Value3" };
        }

    }

    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? EmailAddress { get; set; }
    }
}
