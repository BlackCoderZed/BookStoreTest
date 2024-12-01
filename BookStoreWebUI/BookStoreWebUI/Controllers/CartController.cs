using BookStoreWebUI.Models.Cart;
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
    [Authorize]
    public class CartController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> AddCart(ReqCartUpdateInfo model)
        {
            ResCartUpdateInfo response = await DataAccess.DataAccess.UpdateCart(LoginUserInfo, model, eCartOption.Add);

            return Json(new JsonResponse() { Success = response.Result.Code == Constants.ACK_RESULT, Message = response.Result.Message });
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            ResGetCartInfo response = await DataAccess.DataAccess.GetCartInfo(LoginUserInfo);

            return View("~/Views/Cart/Index.cshtml", response.CartInfos);
        }

        [HttpPost]
        public async Task<ActionResult> CheckOut(ReqCheckOutInfo model)
        {
            ResCheckOut response = await DataAccess.DataAccess.CheckOut(LoginUserInfo, model);

            return Json(new JsonResponse() { Success = response.Result.Code == Constants.ACK_RESULT, Message =response.Result.Message });
        }
    }
}