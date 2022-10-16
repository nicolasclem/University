using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UniversityDBContext _context;
        public AccountController(UniversityDBContext context ,JwtSettings jwtSettings )
        {
            _jwtSettings = jwtSettings;
            _context = context; 
        }

        // example Users  hardcode
        //TODO:  Change by real Users in DB
        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id=1,
                Email="bauti@test.com",
                Name="Admin",
                Password="Admin"

            },
            new User()
            {
                Id=2,
                Email="nico@test.com",
                Name="User1",
                Password="nico123"

            }

        };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();
                
                var Valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                if (Valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId=Guid.NewGuid(),


                    }, _jwtSettings) ;
                }
                else
                {
                    return BadRequest("WORNG CREDENTIAL");
                }

                return Ok(Token);
            
            }
            catch (Exception ex)
            {

                throw new Exception("Get Token Error", ex);
            }
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="Admin")]

        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }

    }
}
