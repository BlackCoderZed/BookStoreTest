using Azure;
using BookStoreDataAccess.DAO;
using BookStoreDataAccess.Models.BookModels.Response;
using BookStoreDataAccess.Models.Common;
using BookStoreWebAPI.Models.Book.Request;
using BookStoreWebAPI.Models.Book.Response;
using BookStoreWebAPI.Utils;
using System.IO;

namespace BookStoreWebAPI.Services
{
    public class BookServices : BaseServices
    {
        internal ResGetBook GetBook(int id)
        {
            ResGetBook response = new ResGetBook();

            try
            {
                ModelValidator.ValidateBookID(id);

                DbBookInfo bookInfo = BookDao.GetBookInfo(id);

                response.BookInfo = ModelConverter.CreateAPIBookInfo(bookInfo);

                response.Result = CreateResult(Constants.ACK_RESULT);
            }
            catch (Exception ex)
            {
                response.Result = CreateResult(Constants.NACK_RESULT, ex.Message);
            }

            return response;
        }

        internal ResGetBookList GetBookList(BookFilterInfo filterInfo)
        {
            ResGetBookList response = new ResGetBookList();

            try
            {
                ModelValidator.ValidateBookFilterInfo(filterInfo);

                DbFilterInfo dbBookFilterInfo = ModelConverter.CreateDbBookFilterInfo(filterInfo);

                ResDbBookInfoList bookResult = BookDao.GetBookInfoList(dbBookFilterInfo);

                response.TotalRecordCount = bookResult.TotalRecord;
                response.BookInfos = ModelConverter.CreateAPIBookInfoList(bookResult.BookList);

                response.Result = CreateResult(Constants.ACK_RESULT);
            }
            catch (Exception ex)
            {
                response.Result = CreateResult(Constants.NACK_RESULT, ex.Message);
            }

            return response;
        }
    }
}
