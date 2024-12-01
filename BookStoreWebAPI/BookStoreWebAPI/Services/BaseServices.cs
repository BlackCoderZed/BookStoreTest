using BookStoreWebAPI.Models.Common;

namespace BookStoreWebAPI.Services
{
    public class BaseServices
    {
        protected Result CreateResult(string resultCode, string message = null)
        {
            Result result = new Result();

            result.Code = resultCode;
            result.Message = message;

            return result;
        }
    }
}
