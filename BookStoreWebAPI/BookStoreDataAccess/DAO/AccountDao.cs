using BookStoreDataAccess.Models;
using BookStoreDataAccess.Models.Account;
using CommonExtension.Exceptions;
using CommonExtension.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDataAccess.DAO
{
    public class AccountDao : BaseDao
    {
        /// <summary>
        /// Authenciate
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public static User Authenciate(AccountLoginInfo loginInfo)
        {
            try
            {
                using (BookStoreEntities db = new BookStoreEntities())
                {
                    string encryptedPassword = CypherAndHashManager.Encrypt(loginInfo.Password);

                    User user = db.Users
                        .Where(W => W.Email == loginInfo.Email)
                        .Where(W => W.Password == encryptedPassword)
                        .FirstOrDefault();

                    if (user == null)
                    {
                        throw new AppException(AppException.MSG_INVALID_LOGIN);
                    }

                    if (user.DelFlg != null)
                    {
                        throw new AppException(AppException.MSG_ACCOUNT_TERMINATED);
                    }

                    return user;
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                // create error log ex
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// CheckUserExist
        /// </summary>
        /// <param name="userId"></param>
        public static void CheckUserExist(int userId)
        {
            try
            {
                using (BookStoreEntities db = new BookStoreEntities())
                {
                    User user = db.Users
                        .Where(W => W.Id == userId)
                        .FirstOrDefault();

                    if (user == null)
                    {
                        throw new AppException(AppException.MSG_INVALID_LOGIN);
                    }

                    if (user.DelFlg != null)
                    {
                        throw new AppException(AppException.MSG_ACCOUNT_TERMINATED);
                    }
                }
            }
            catch (Exception ex)
            {
                // create error log ex
                throw;
            }
        }
    }
}
