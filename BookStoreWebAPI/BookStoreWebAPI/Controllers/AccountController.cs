using BookStoreWebAPI.Models.Account;
using BookStoreWebAPI.Models.Account.Response;
using BookStoreWebAPI.Services;
using BookStoreWebAPI.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserLoginInfo userLogin)
        {
            AccountServices service = new AccountServices();
            ResUserLogin result = service.Authentiate(userLogin);

            if (result.Result.Code == Constants.ACK_RESULT)
            {
                result.AuthToken = CreateJWTToken(userLogin.Email);
            }

            return Ok(result);
        }
    }
}
