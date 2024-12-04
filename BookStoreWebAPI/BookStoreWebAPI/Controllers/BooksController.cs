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
        private readonly BookServices _bookService;

        public BooksController(BookServices bookService)
        {
            _bookService = bookService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetBooks")]
        public IActionResult GetBookList([FromQuery]BookFilterInfo filterInfo)
        {
            ResGetBookList response = _bookService.GetBookList(filterInfo);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Detail/{id}")]
        public IActionResult GetBook(int id)
        {
            ResGetBook response = _bookService.GetBook(id);
            
            return Ok(response);
        }
    }
}
