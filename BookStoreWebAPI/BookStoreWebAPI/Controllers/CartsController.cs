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
    public class CartsController : ControllerBase
    {
        private readonly CartServices _cartService;

        public CartsController(CartServices cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        [Route("UpdateCart")]
        public IActionResult AddOrUpCart([FromBody] ReqAddCartInfo model)
        {
            ResAddCart response = _cartService.AddOrUpdateCart(model);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetCart")]
        public IActionResult GetCart([FromQuery] ReqGetCart model)
        {
            ResGetCart response = _cartService.GetCartInfo(model);

            return Ok(response);
        }

        [HttpPost]
        [Route("CheckOut")]
        public IActionResult CheckOut([FromBody] ReqCheckOut model)
        {
            ResCheckOut response = _cartService.CheckOut(model);

            return Ok(response);
        }
    }
}
