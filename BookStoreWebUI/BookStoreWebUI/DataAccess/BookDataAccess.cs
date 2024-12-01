using BookStoreWebUI.Models.Books;
using BookStoreWebUI.Models.Common;
using BookStoreWebUI.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookStoreWebUI.DataAccess
{
    public partial class DataAccess
    {
        internal static async Task<BookViewModel> GetBookAsync(int id)
        {
            string query = CreateApiGetBookQuery(id);
            string jsonResult = await ApiAccessHelper.SendGetRequestAsync(Constants.BOOK_GET_URL, query, null);

            //CheckResult(jsonResult);

            BookViewModel bookModel = JsonConvert.DeserializeObject<BookViewModel>(jsonResult);
            return bookModel;
        }


        internal static async Task<BookListViewModel> GetBookListAsync(APIBaseFilterInfo filterInfo)
        {
            string query = CreateApiGetBookListFilterQuery(filterInfo);
            string json = await ApiAccessHelper.SendGetRequestAsync(Constants.BOOK_LIST_GET_URL, query, null);

            //CheckResult(json);

            BookListViewModel bookViewModel = JsonConvert.DeserializeObject<BookListViewModel>(json);
            return bookViewModel;
        }

        private static string CreateApiGetBookListFilterQuery(APIBaseFilterInfo filterInfo)
        {
            if (filterInfo == null)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append($@"?Start={filterInfo.Start}&Length={filterInfo.Length}&SortColumn={filterInfo.SortColumn}&SortOrder={filterInfo.SortOrder}");
            
            if (!string.IsNullOrEmpty(filterInfo.SearchValue))
            {
                sb.Append($@"&SearchValue={filterInfo.SearchValue}");
            }

            return sb.ToString();
        }


        private static string CreateApiGetBookQuery(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($@"/{id}");

            return sb.ToString();
        }
    }
}