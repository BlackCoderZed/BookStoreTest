using BookStoreWebAPI.Models.Common;

namespace BookStoreWebAPI.Models.Book.Response
{
    public class ResGetBook : ResultBase
    {
        public BookInfo BookInfo { get; set; }
    }
}
