using BookStoreWebUI.Models.Books;
using BookStoreWebUI.Models.Common;
using BookStoreWebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookStoreWebUI.Controllers
{
    public class BookController : BaseController
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetBookList()
        {
            APIBaseFilterInfo filterInfo = CreateGridviewViewFilter(Request.Form, typeof(eBookGridview));
            BookListViewModel bookModel = await DataAccess.DataAccess.GetBookListAsync(filterInfo);

            GridviewJsonResponse jsonResponse = CreateGridviewJsonResponse(filterInfo.Draw, bookModel);

            return Json(jsonResponse);
        }

        [HttpGet]
        public async Task<ActionResult> Detail(int id)
        {
            BookViewModel bookModel = await DataAccess.DataAccess.GetBookAsync(id);

            return View("~/Views/Book/Detail.cshtml", bookModel.Book);
        }
    }
}