using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreWebAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IConfiguration _config;

        public BaseController(IConfiguration configuration)
        {
            _config = configuration;
        }

        
    }
}
