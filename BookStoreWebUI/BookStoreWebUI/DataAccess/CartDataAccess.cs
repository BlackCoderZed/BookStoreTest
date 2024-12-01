using BookStoreWebUI.Models.Account;
using BookStoreWebUI.Models.Cart;
using BookStoreWebUI.Models.Common;
using BookStoreWebUI.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BookStoreWebUI.DataAccess
{
    public partial class DataAccess
    {
        public static async Task<ResCartUpdateInfo> UpdateCart(LoginUserInfo loginUserInfo, ReqCartUpdateInfo model, eCartOption option)
        {
            ApiCartUpdateModel apiModel = CreateAPICartUpdateModel(loginUserInfo.LoginID, model, option); 
            string jsonResponse = await ApiAccessHelper.SendPostRequestAsync(Constants.CART_UPDATE_URL, apiModel, loginUserInfo.Token);

            //CheckResult(jsonResponse);

            ResCartUpdateInfo response = JsonConvert.DeserializeObject<ResCartUpdateInfo>(jsonResponse);

            return response;
        }

        public static async Task<ResGetCartInfo> GetCartInfo(LoginUserInfo loginUserInfo)
        {
            string query = CreateApiGetCartQuery(loginUserInfo.LoginID);
            string jsonResult = await ApiAccessHelper.SendGetRequestAsync(Constants.CART_GET_URL, query, loginUserInfo.Token);
            //CheckResult(jsonResult);

            ResGetCartInfo response = JsonConvert.DeserializeObject<ResGetCartInfo>(jsonResult);
            return response;
        }

        public static async Task<ResCheckOut> CheckOut(LoginUserInfo loginUserInfo, ReqCheckOutInfo model)
        {
            ApiCheckOutModel apiModel = CreateApiCheckoutModel(loginUserInfo.LoginID, model);
            string jsonResponse = await ApiAccessHelper.SendPostRequestAsync(Constants.CHECK_OUT_URL, apiModel, loginUserInfo.Token);

            //CheckResult(jsonResponse);

            ResCheckOut response = JsonConvert.DeserializeObject<ResCheckOut>(jsonResponse);
            return response;
        }

        private static ApiCheckOutModel CreateApiCheckoutModel(int loginID, ReqCheckOutInfo model)
        {
            ApiCheckOutModel apiModel = new ApiCheckOutModel();
            apiModel.UserID = loginID;
            apiModel.CartIds = new List<int>() { model.CartId };

            return apiModel;
        }

        private static string CreateApiGetCartQuery(int loginID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($@"/?UserId={loginID}");

            return sb.ToString();
        }

        private static ApiCartUpdateModel CreateAPICartUpdateModel(int loginID, ReqCartUpdateInfo model, eCartOption option)
        {
            ApiCartUpdateModel apiModel = new ApiCartUpdateModel();
            apiModel.UserId = loginID;
            apiModel.BookId = model.BookId;
            apiModel.Qty = model.Qty;
            apiModel.Option = (byte)option;

            return apiModel;
        }
    }
}