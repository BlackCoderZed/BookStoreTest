using BookStoreWebAPI.Models.Common;

namespace BookStoreWebAPI.Models.Cart.Response
{
    public class ResGetCart : ResultBase
    {
        public List<CartInfo> CartInfos { get; set; }
    }
}
