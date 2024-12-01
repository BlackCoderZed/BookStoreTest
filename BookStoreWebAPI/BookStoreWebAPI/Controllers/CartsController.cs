using BookStoreWebAPI.Models.Cart;
using BookStoreWebAPI.Models.Cart.Request;
using BookStoreWebAPI.Models.Cart.Response;
using BookStoreWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : BaseController
    {
        public CartsController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpPost]
        [Route("UpdateCart")]
        public IActionResult AddOrUpCart([FromBody] ReqAddCartInfo model)
        {
            CartServices service = new CartServices();
            ResAddCart response = service.AddOrUpdateCart(model);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetCart")]
        public IActionResult GetCart([FromQuery] ReqGetCart model)
        {
            CartServices service = new CartServices();
            ResGetCart response = service.GetCartInfo(model);

            return Ok(response);
        }

        [HttpPost]
        [Route("CheckOut")]
        public IActionResult CheckOut([FromBody] ReqCheckOut model)
        {
            CartServices service = new CartServices();
            ResCheckOut response = service.CheckOut(model);

            return Ok(response);
        }
    }
}
