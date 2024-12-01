using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWebUI.Models.Cart
{
    public class ReqCartUpdateInfo
    {
        public int BookId { get; set; }

        public int Qty { get; set; }
    }
}