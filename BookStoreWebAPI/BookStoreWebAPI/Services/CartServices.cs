using BookStoreDataAccess.DAO;
using BookStoreDataAccess.Models.CartModels;
using BookStoreDataAccess.Models.CartModels.Request;
using BookStoreWebAPI.Models.Cart;
using BookStoreWebAPI.Models.Cart.Request;
using BookStoreWebAPI.Models.Cart.Response;
using BookStoreWebAPI.Utils;
using CommonExtension.Utils;

namespace BookStoreWebAPI.Services
{
    public class CartServices : BaseServices
    {
        internal ResAddCart AddOrUpdateCart(ReqAddCartInfo model)
        {
            ResAddCart response = new ResAddCart();

            try
            {
                ModelValidator.ValidateCartUpdateInfo(model);

                AccountDao.CheckUserExist(model.UserId);

                BookDao.CheckExist(model.BookId);

                if (model.Option == (byte)eCartOption.Add)
                {
                    BookDao.CheckRequestQty(model.BookId, model.Qty);
                }
                
                if (model.Option == (byte)eCartOption.Remove)
                {
                    CartDao.CheckRequestInfo(model.UserId, model.BookId, model.Qty);   
                }

                ReqDBAddCartInfo dbModel = ModelConverter.CreateDBAddCardInfo(model);

                CartDao.AddOrUpdateCart(dbModel);

                response.Result = CreateResult(Constants.ACK_RESULT);
            }
            catch (Exception ex)
            {
                response.Result = CreateResult(Constants.NACK_RESULT, ex.Message);
            }

            return response;
        }

        
        internal ResCheckOut CheckOut(ReqCheckOut model)
        {
            ResCheckOut response = new ResCheckOut();

            try
            {
                ModelValidator.ValidateCheckOutInfo(model);

                AccountDao.CheckUserExist(model.UserId);

                CartDao.CheckCartIds(model.CartIds);

                List<DbCartInfo> dbCartInfoList = CartDao.GetCartInfo(model.CartIds);

                BookDao.CheckBalance(dbCartInfoList);

                double totalAmount = CalculateTotalAmount(dbCartInfoList);

                CartDao.CheckOut(dbCartInfoList, model.UserId, totalAmount);

                response.Result = CreateResult(Constants.ACK_RESULT);
            }
            catch (Exception ex)
            {
                response.Result = CreateResult(Constants.NACK_RESULT, ex.Message);
            }

            return response;
        }

        private double CalculateTotalAmount(List<DbCartInfo> dbCartInfoList)
        {
            double amount = 0;

            foreach (DbCartInfo info in dbCartInfoList)
            {
                amount += info.Qty * info.Price;
            }

            return amount;
        }

        internal ResGetCart GetCartInfo(ReqGetCart model)
        {
            ResGetCart response = new ResGetCart();

            try
            {
                ModelValidator.ValidateReqGetCart(model);
                AccountDao.CheckUserExist(model.UserId);

                List<DbCartInfo> dbCarts = CartDao.GetCartInfo(model.UserId);
                response.CartInfos = ModelConverter.CreateAPICartInfo(dbCarts);
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
