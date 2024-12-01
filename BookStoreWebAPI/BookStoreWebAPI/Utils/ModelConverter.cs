using BookStoreDataAccess.Models.Account;
using BookStoreDataAccess.Models.BookModels.Response;
using BookStoreDataAccess.Models.CartModels;
using BookStoreDataAccess.Models.CartModels.Request;
using BookStoreDataAccess.Models.Common;
using BookStoreWebAPI.Models.Account;
using BookStoreWebAPI.Models.Book;
using BookStoreWebAPI.Models.Book.Request;
using BookStoreWebAPI.Models.Cart;
using BookStoreWebAPI.Models.Cart.Request;

namespace BookStoreWebAPI.Utils
{
    public class ModelConverter
    {
        internal static BookInfo CreateAPIBookInfo(DbBookInfo bookInfo)
        {
            BookInfo book = new BookInfo();

            if (bookInfo == null)
            {
                return book;
            }

            book.Id = bookInfo.Id;
            book.Title = bookInfo.Title;
            book.Description = bookInfo.Description;
            book.Author = bookInfo.Author;
            book.ReleaseDate = bookInfo.ReleaseDate;
            book.Category = bookInfo.Category;
            book.Price = bookInfo.Price;
            book.Qty = bookInfo.Qty;
            book.ImageUrl = bookInfo.ImageUrl;

            return book;
        }

        internal static List<BookInfo> CreateAPIBookInfoList(List<DbBookInfo> bookList)
        {
            List<BookInfo> bookInfoList = new List<BookInfo>();

            if (bookList == null)
            {
                return bookInfoList;
            }

            bookInfoList = bookList.Select(S => new BookInfo()
            {
                Id = S.Id,
                Title = S.Title,
                Author = S.Author,
                Category = S.Category,
                Description = S.Description,
                ReleaseDate = S.ReleaseDate,
                Price = S.Price,
                Qty = S.Qty,
                ImageUrl = S.ImageUrl,
            }).ToList();

            return bookInfoList;
        }

        internal static List<CartInfo> CreateAPICartInfo(List<DbCartInfo> dbCarts)
        {
            List<CartInfo> carts = new List<CartInfo>();

            if (dbCarts == null)
            {
                return carts;
            }

            foreach(DbCartInfo cart in dbCarts)
            {
                CartInfo cartInfo = new CartInfo();
                cartInfo.Id = cart.Id;
                cartInfo.BookId = cart.BookId;
                cartInfo.Title = cart.Title;
                cartInfo.Description = cart.Description;
                cartInfo.Author = cart.Author;
                cartInfo.Price = cart.Price;
                cartInfo.Qty = cart.Qty;
                cartInfo.ImageUrl = cart.ImageUrl;
                cartInfo.TotalAmount = cart.Qty * cart.Price;

                carts.Add(cartInfo);
            }

            return carts;
        }

        internal static ReqDBAddCartInfo CreateDBAddCardInfo(ReqAddCartInfo model)
        {
            ReqDBAddCartInfo dbInfo = new ReqDBAddCartInfo();

            if (model == null)
            {
                return dbInfo;
            }

            dbInfo.Option = model.Option;
            dbInfo.UserId = model.UserId;
            dbInfo.BookId = model.BookId;
            dbInfo.Qty = model.Qty;

            return dbInfo;
        }

        /// <summary>
        /// CreateDbBookFilterInfo
        /// </summary>
        /// <param name="filterInfo"></param>
        /// <returns></returns>
        internal static DbFilterInfo CreateDbBookFilterInfo(BookFilterInfo filterInfo)
        {
            DbFilterInfo filter = new DbFilterInfo();

            if (filterInfo == null)
            {
                return filter;
            }

            filter.Start = filterInfo.Start;
            filter.Length = filterInfo.Length;
            filter.SortOrder = filterInfo.SortOrder;
            filter.SortColumn = filterInfo.SortColumn;
            filter.SearchValue = filterInfo.SearchValue;

            return filter;
        }

        internal static AccountLoginInfo CreateDbUserLoginInfo(UserLoginInfo userLogin)
        {
            AccountLoginInfo accountLoginInfo = new AccountLoginInfo();

            if (userLogin == null)
            {
                return accountLoginInfo;
            }

            accountLoginInfo.Email = userLogin.Email;
            accountLoginInfo.Password = userLogin.Password;

            return accountLoginInfo;
        }
    }
}
