namespace BookStoreWebAPI.Models.Cart.Request
{
    public class ReqCheckOut
    {
        public int UserId { get; set; }

        public List<int> CartIds { get; set; }
    }
}
