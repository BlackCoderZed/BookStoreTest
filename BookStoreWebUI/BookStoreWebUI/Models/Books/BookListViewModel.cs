using BookStoreWebUI.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWebUI.Models.Books
{
    public class BookListViewModel : ResultBase
    {
        public int TotalRecordCount { get; set; }

        [JsonProperty("bookInfos")]
        public List<BookInfo> BookList { get; set; }
    }
}