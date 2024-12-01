using BookStoreDataAccess.DAO;
using BookStoreDataAccess.Models;
using BookStoreDataAccess.Models.Account;
using BookStoreWebAPI.Models.Account;
using BookStoreWebAPI.Models.Account.Response;
using BookStoreWebAPI.Utils;

namespace BookStoreWebAPI.Services
{
    public class AccountServices : BaseServices
    {
        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        internal ResUserLogin Authentiate(UserLoginInfo userLogin)
        {
            ResUserLogin response = new ResUserLogin();

            try
            {
                ModelValidator.ValidateUserLoginInfo(userLogin);

                AccountLoginInfo dbLoginInfo = ModelConverter.CreateDbUserLoginInfo(userLogin);

                User user = AccountDao.Authenciate(dbLoginInfo);

                response.UserID = user.Id;
                response.UserDlpName = user.UserDlpName;

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
