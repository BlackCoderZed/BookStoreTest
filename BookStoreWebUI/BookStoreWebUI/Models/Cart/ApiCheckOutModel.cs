using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWebUI.Models.Cart
{
    public class ApiCheckOutModel
    {
        public int UserID { get; set; }
        public List<int> CartIds { get; set; }
    }
}