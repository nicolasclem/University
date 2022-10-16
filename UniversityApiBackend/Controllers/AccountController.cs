using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSetting;
        public AccountController(JwtSettings jwtSetting)
        {
            _jwtSetting = jwtSetting;
        }

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
                Name="User 1",
                Password="nico123"

            }

        };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {
                var Token = new UserTokens();
                var valid = Logins.Any(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));

                if (valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId=Guid.NewGuid(),


                    }, _jwtSetting) ;
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
