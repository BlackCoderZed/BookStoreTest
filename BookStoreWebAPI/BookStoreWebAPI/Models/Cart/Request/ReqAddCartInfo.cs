namespace BookStoreWebAPI.Models.Cart.Request
{
    public class ReqAddCartInfo
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public int Qty { get; set; }

        public byte Option { get; set; }
    }
}
