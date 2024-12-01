using BookStoreWebUI.Models.Books;
using BookStoreWebUI.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWebUI.DataAccess
{
    public partial class DataAccess
    {
        internal static void CheckResult(string response)
        {
            dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(response);

            var result = jsonObj["result"];

            if (result["code"] == Constants.NACK_RESULT)
            {
                string message = result["message"];
                throw new Exception(message);
            }
        }
    }
}