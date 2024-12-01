using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWebUI.Models.Cart
{
    public class ApiCartUpdateModel
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public int Qty { get; set; }

        public byte Option { get; set; }
    }
}