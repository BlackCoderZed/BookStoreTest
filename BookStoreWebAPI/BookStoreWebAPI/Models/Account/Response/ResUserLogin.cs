using BookStoreWebAPI.Models.Common;

namespace BookStoreWebAPI.Models.Account.Response
{
    public class ResUserLogin : ResultBase
    {
        public int UserID { get; set; }

        public string UserDlpName { get; set; }

        public string AuthToken { get; set; }
    }
}
