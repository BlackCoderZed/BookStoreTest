using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDataAccess.Models.CartModels
{
    public class DbCartInfo
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public int Qty { get; set; } // from cart

        public double Price { get; set; }

    }
}
