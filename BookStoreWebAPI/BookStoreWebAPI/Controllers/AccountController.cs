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
    public class AccountController : ControllerBase
    {
        private readonly AccountServices _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration, AccountServices accountService)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserLoginInfo userLogin)
        {
            ResUserLogin result = _accountService.Authentiate(userLogin, _configuration);

            return Ok(result);
        }
    }
}
