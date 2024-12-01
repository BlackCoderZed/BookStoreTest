namespace BookStoreWebAPI.Models.Book
{
    public class BookInfo
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
