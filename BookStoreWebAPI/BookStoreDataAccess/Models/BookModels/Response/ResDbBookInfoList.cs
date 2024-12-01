using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDataAccess.Models.BookModels.Response
{
    public class ResDbBookInfoList
    {
        public int TotalRecord {  get; set; }


        public List<DbBookInfo> BookList { get; set; }
    }
}
