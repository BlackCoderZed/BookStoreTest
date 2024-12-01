using BookStoreWebUI.Models.Account;
using BookStoreWebUI.Models.Books;
using BookStoreWebUI.Models.Common;
using BookStoreWebUI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Ast;

namespace BookStoreWebUI.Controllers
{
    public class BaseController : Controller
    {
        public LoginUserInfo LoginUserInfo
        {
            get { return GetLoginUserInfo(); }
        }

        private LoginUserInfo GetLoginUserInfo()
        {
            if (Session[Constants.SESSION_WEB_LOGIN_INFO] == null)
            {
                throw new Exception("Invalid login");
            }
            return (LoginUserInfo)Session[Constants.SESSION_WEB_LOGIN_INFO];
        }

        public APIBaseFilterInfo CreateGridviewViewFilter(NameValueCollection value, Type gridviewType)
        {
            var draw = int.Parse(value.GetValues("draw").FirstOrDefault());
            var start = int.Parse(value.GetValues("start").FirstOrDefault());
            var lenght = int.Parse(value.GetValues("length").FirstOrDefault());
            var sortCol = int.Parse(value.GetValues("order[0][column]").FirstOrDefault());
            var sorDir = value.GetValues("order[0][Dir]").FirstOrDefault();
            var searchVal = value.GetValues("search[value]").FirstOrDefault();

            // 
            string sortColumnName = Enum.GetName(gridviewType, sortCol);

            APIBaseFilterInfo gridviewFilter = new APIBaseFilterInfo();

            //gridviewFilter.Draw = draw;
            gridviewFilter.Start = start;
            gridviewFilter.Length = lenght == 0 ? 10 : lenght;
            gridviewFilter.SortOrder = sorDir == string.Empty ? "asc" : sorDir;
            gridviewFilter.SortColumn = sortColumnName;
            gridviewFilter.SearchValue = searchVal;

            return gridviewFilter;
        }

        protected GridviewJsonResponse CreateGridviewJsonResponse(int draw, BookListViewModel model)
        {
            GridviewJsonResponse response = new GridviewJsonResponse();

            response.Success = model.Result.Code == Constants.ACK_RESULT;
            response.Message = model.Result.Message;

            response.draw = draw;
            response.recordsTotal = model.TotalRecordCount;
            response.recordsFiltered = model.TotalRecordCount;
            response.data = model.BookList;

            return response;
        }
    }
}