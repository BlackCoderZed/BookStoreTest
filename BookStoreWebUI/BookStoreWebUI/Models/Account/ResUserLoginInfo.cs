using BookStoreWebUI.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWebUI.Models.Account
{
    public class ResUserLoginInfo : ResultBase
    {
        [JsonProperty("userID")]
        public int UserID { get; set; }

        [JsonProperty("userDlpName")]
        public string UserDlpName { get; set; }

        [JsonProperty("authToken")]
        public string AuthToken { get; set; }
    }
}