using BookStoreWebAPI.Models.Common;

namespace BookStoreWebAPI.Models.Book.Response
{
    public class ResGetBookList : ResultBase
    {
        public int TotalRecordCount { get; set; }

        public List<BookInfo> BookInfos { get; set; }
    }
}
