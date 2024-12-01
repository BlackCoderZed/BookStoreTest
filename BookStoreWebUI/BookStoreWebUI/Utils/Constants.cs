using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWebUI.Utils
{
    public class Constants
    {
        public const string CONFIG_PATH = @"App_Data\Config.xml";

        public const string BOOK_LIST_GET_URL = "api/Books/GetBooks";

        public const string BOOK_GET_URL = "api/Books/Detail";

        public const string LOGIN_URL = "api/Account/Login";

        public const string CART_UPDATE_URL = "api/Carts/UpdateCart";

        public const string CART_GET_URL = "api/Carts/GetCart";

        public const string CHECK_OUT_URL = "api/Carts/CheckOut";

        public const string ACK_RESULT = "1";

        public const string NACK_RESULT = "2";

        public const string SESSION_WEB_LOGIN_INFO = "SESSION_WEB_LOGIN_INFO";
    }
}