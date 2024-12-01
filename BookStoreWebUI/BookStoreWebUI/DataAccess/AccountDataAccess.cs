using BookStoreWebUI.Models.Account;
using BookStoreWebUI.Models.Cart;
using BookStoreWebUI.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BookStoreWebUI.DataAccess
{
    public partial class DataAccess
    {
        public static async Task<ResUserLoginInfo> Authenticate(WebLoginReqInfo model)
        {
            string jsonResponse = await ApiAccessHelper.SendPostRequestAsync(Constants.LOGIN_URL, model, null);

            CheckResult(jsonResponse);

            ResUserLoginInfo response = JsonConvert.DeserializeObject<ResUserLoginInfo>(jsonResponse);

            return response;
        }

    }
}