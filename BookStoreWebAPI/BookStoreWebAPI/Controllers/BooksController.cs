using BookStoreWebAPI.Models.Book.Request;
using BookStoreWebAPI.Models.Book.Response;
using BookStoreWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("GetBooks")]
        public IActionResult GetBookList([FromQuery]BookFilterInfo filterInfo)
        {
            BookServices services = new BookServices();
            ResGetBookList response = services.GetBookList(filterInfo);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Detail/{id}")]
        public IActionResult GetBook(int id)
        {
            BookServices services = new BookServices();
            ResGetBook response = services.GetBook(id);
            
            return Ok(response);
        }
    }
}
