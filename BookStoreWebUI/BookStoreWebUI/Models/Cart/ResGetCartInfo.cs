using BookStoreWebUI.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWebUI.Models.Cart
{
    public class ResGetCartInfo : ResultBase
    {
        [JsonProperty("cartInfos")]
        public List<CartInfo> CartInfos { get; set; }
    }
}