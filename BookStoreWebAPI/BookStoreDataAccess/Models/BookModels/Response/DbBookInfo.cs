using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDataAccess.Models.BookModels.Response
{
    public class DbBookInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public Double Price { get; set; }

        public DateOnly? ReleaseDate { get; set; }

        public int Qty { get; set; }

        public string ImageUrl { get; set; }
    }
}
