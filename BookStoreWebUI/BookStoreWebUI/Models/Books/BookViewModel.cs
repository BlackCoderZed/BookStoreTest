using BookStoreWebUI.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWebUI.Models.Books
{
    public class BookViewModel : ResultBase
    {
        [JsonProperty("bookInfo")]
        public BookInfo Book { get; set; }
    }
}