namespace BookStoreWebAPI.Models.Cart
{
    public class CartInfo
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public int Qty { get; set; } // from cart

        public double Price { get; set; }

        public double TotalAmount { get; set; } // qty * price
    }
}
