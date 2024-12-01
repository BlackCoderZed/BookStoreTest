using BookStoreWebAPI.Models.Account;
using BookStoreWebAPI.Models.Book.Request;
using BookStoreWebAPI.Models.Cart.Request;
using CommonExtension.Exceptions;
using CommonExtension.Utils;

namespace BookStoreWebAPI.Utils
{
    public class ModelValidator
    {
        internal static void ValidateBookFilterInfo(BookFilterInfo filterInfo)
        {
            try
            {
                if (filterInfo == null)
                {
                    throw new AppException(AppException.MSG_INPUT_INVALID);
                }

                if (filterInfo.Length < Constants.MIN_FILTER_LENGTH || filterInfo.Length > Constants.MAX_FILTER_LENGHT)
                {
                    throw new AppException(string.Format(AppException.MSG_INVALID_RANGE, nameof(filterInfo.Length)));
                }

                if (filterInfo.Start < Constants.MIN_START_INDEX)
                {
                    throw new AppException(string.Format(AppException.MSG_INVALID_RANGE, nameof(filterInfo.Start)));
                }

                if (string.IsNullOrEmpty(filterInfo.SortColumn) || string.IsNullOrWhiteSpace(filterInfo.SortColumn))
                {
                    throw new AppException(string.Format(AppException.MSG_PARAM_VALUE_INVALID, nameof(filterInfo.SortColumn)));
                }

                if (string.IsNullOrEmpty(filterInfo.SortOrder) || string.IsNullOrWhiteSpace(filterInfo.SortOrder))
                {
                    throw new AppException(string.Format(AppException.MSG_PARAM_VALUE_INVALID, nameof(filterInfo.SortOrder)));
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static void ValidateBookID(int id)
        {
            try
            {
                if (id < Constants.MIN_START_INDEX)
                {
                    throw new AppException(AppException.MSG_INPUT_INVALID);
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static void ValidateCartUpdateInfo(ReqAddCartInfo model)
        {
            try
            {
                if (model == null)
                {
                    throw new AppException(AppException.MSG_INPUT_INVALID);
                }

                if (model.UserId <= Constants.MIN_START_INDEX)
                {
                    throw new AppException(string.Format(AppException.MSG_PARAM_VALUE_INVALID, nameof(model.UserId)));
                }

                if (model.BookId <= Constants.MIN_START_INDEX)
                {
                    throw new AppException(string.Format(AppException.MSG_PARAM_VALUE_INVALID, nameof(model.BookId)));
                }

                if (model.Qty <= Constants.MIN_START_INDEX)
                {
                    throw new AppException(string.Format(AppException.MSG_PARAM_VALUE_INVALID, nameof(model.Qty)));
                }

                if (model.Option != (byte)eCartOption.Add && model.Option != (byte)eCartOption.Remove)
                {
                    throw new AppException(string.Format(AppException.MSG_PARAM_VALUE_INVALID, nameof(model.Option)));
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static void ValidateCheckOutInfo(ReqCheckOut model)
        {
            try
            {
                if (model == null)
                {
                    throw new AppException(AppException.MSG_INPUT_INVALID);
                }

                if (model.UserId <= Constants.MIN_START_INDEX)
                {
                    throw new AppException(string.Format(AppException.MSG_PARAM_VALUE_INVALID, nameof(model.UserId)));
                }

                if (model.CartIds == null || model.CartIds.Count <= 0)
                {
                    throw new AppException(string.Format(AppException.MSG_PARAM_VALUE_INVALID, nameof(model.CartIds)));
                }

                foreach (int id in model.CartIds)
                {
                    if (id <= Constants.MIN_START_INDEX)
                    {
                        throw new AppException(string.Format(AppException.MSG_PARAM_VALUE_INVALID, nameof(model.CartIds)));
                    }
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static void ValidateReqGetCart(ReqGetCart model)
        {
            try
            {
                if (model == null)
                {
                    throw new AppException(AppException.MSG_INPUT_INVALID);
                }

                if (model.UserId <= Constants.MIN_START_INDEX)
                {
                    throw new AppException(string.Format(AppException.MSG_PARAM_VALUE_INVALID, nameof(model.UserId)));
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static void ValidateUserLoginInfo(UserLoginInfo userLogin)
        {
            try
            {
                if (userLogin == null)
                {
                    throw new AppException(AppException.MSG_INPUT_INVALID);
                }

                if (string.IsNullOrEmpty(userLogin.Email) || string.IsNullOrWhiteSpace(userLogin.Email))
                {
                    throw new AppException(string.Format(AppException.MSG_PARAM_VALUE_INVALID, nameof(userLogin.Email)));
                }

                if (string.IsNullOrEmpty(userLogin.Password) || string.IsNullOrWhiteSpace(userLogin.Password))
                {
                    throw new AppException(string.Format(AppException.MSG_PARAM_VALUE_INVALID, nameof(userLogin.Password)));
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
