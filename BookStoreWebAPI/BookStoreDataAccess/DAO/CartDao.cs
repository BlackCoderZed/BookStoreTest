using BookStoreDataAccess.Models;
using BookStoreDataAccess.Models.CartModels;
using BookStoreDataAccess.Models.CartModels.Request;
using CommonExtension.Exceptions;
using CommonExtension.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BookStoreDataAccess.DAO
{
    public class CartDao : BaseDao
    {
        #region Public Method
        /// <summary>
        /// AddOrUpdateCart
        /// </summary>
        /// <param name="dbModel"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void AddOrUpdateCart(ReqDBAddCartInfo dbModel)
        {
            try
            {
                using (BookStoreEntities context = new BookStoreEntities())
                {
                    Cart cart = context.Carts
                        .Where(W => W.UserId == dbModel.UserId)
                        .Where(W => W.BookId == dbModel.BookId)
                        .FirstOrDefault();
                    
                    if (cart == null && dbModel.Option == (byte)eCartOption.Remove)
                    {
                        return;
                    }

                    bool needAdd = false;

                    if (cart == null && dbModel.Option == (byte)eCartOption.Add)
                    {
                        cart = new Cart();
                        cart.UserId = dbModel.UserId;
                        cart.BookId = dbModel.BookId;
                        needAdd = true;
                    }

                    cart.Qty = dbModel.Option == (byte)eCartOption.Add ? cart.Qty + dbModel.Qty : cart.Qty - dbModel.Qty;

                    if (cart.Qty <= 0 && needAdd)
                    {
                        return;
                    }
                    else if (cart.Qty <= 0 && !needAdd)
                    {
                        context.Carts.Remove(cart);
                    }

                    if (needAdd)
                    {
                        context.Carts.Add(cart);
                    }

                    context.SaveChanges();
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                //log ex
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// Check Card IDs
        /// </summary>
        /// <param name="cartIds"></param>
        /// <exception cref="AppException"></exception>
        public static void CheckCartIds(List<int> cartIds)
        {
            try
            {
                using (BookStoreEntities context = new BookStoreEntities())
                {
                    List<int> existingIds = context.Carts
                        .Where(W => cartIds.Contains(W.Id))
                        .Select(S => S.Id)
                        .ToList();

                    if (existingIds.Count != cartIds.Count)
                    {
                        List<int> notExistIds = cartIds.Except(existingIds).ToList();
                        string invalidIds = string.Join(",", notExistIds);

                        throw new AppException(string.Format(AppException.MSG_NOT_EXIST_CART_ID, invalidIds));
                    }
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// Check out
        /// </summary>
        /// <param name="dbCartInfoList"></param>
        /// <param name="userId"></param>
        /// <param name="totalAmount"></param>
        /// <exception cref="AppException"></exception>
        public static void CheckOut(List<DbCartInfo> dbCartInfoList, int userId, double totalAmount)
        {
            try
            {
                using (BookStoreEntities context = new BookStoreEntities())
                {
                    using (TransactionScope transaction = GetReadCommitmentTransactionScope())
                    {
                        // insert order
                        int orderId = InsertOrderInfo(context, dbCartInfoList, userId, totalAmount);

                        InsertOrderDetailInfo(context, dbCartInfoList, orderId);
                        
                        // remove card ids
                        List<int> cartIds = dbCartInfoList.Select(S => S.Id).ToList();
                        CartDao.RemoveCartInfo(context, cartIds);

                        BookDao.UpdateQty(context, dbCartInfoList);

                        transaction.Complete();
                    }
                }
            }
            catch (Exception)
            {
                throw new AppException(AppException.MSG_FAIL);
            }
        }

        /// <summary>
        /// Check Item is exist and qty
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <param name="reqQty"></param>
        /// <exception cref="AppException"></exception>
        public static void CheckRequestInfo(int userId, int bookId, int reqQty)
        {
            try
            {
                using (BookStoreEntities context = new BookStoreEntities())
                {
                    Cart cart = context.Carts
                        .Where(W => W.BookId.Equals(bookId))
                        .Where(W => W.UserId.Equals(userId))
                        .FirstOrDefault();

                    if (cart == null)
                    {
                        throw new AppException(string.Format(AppException.MSG_NOT_EXIST, typeof(Book).Name));
                    }

                    if (cart.Qty < reqQty)
                    {
                        throw new AppException(AppException.MSG_INVALID_OPERATION);
                    }
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                //log ex
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// Get Cart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public static List<DbCartInfo> GetCartInfo(int userId)
        {
            try
            {
                using (BookStoreEntities context = new BookStoreEntities())
                {
                    List<DbCartInfo> cartInfoList = context.Carts
                        .Where(W => W.UserId == userId)
                        .Join(context.Books, c => c.BookId, b => b.Id, (c, b) => new { c, b })
                        .Select(S => new DbCartInfo()
                        {
                            Id = S.c.Id,
                            BookId = S.c.BookId,
                            Title = S.b.Title,
                            Description = S.b.Description,
                            Author = S.b.Author,
                            ImageUrl = S.b.ImageUrl,
                            Qty = S.c.Qty,
                            Price = S.b.Price,
                        }).ToList();

                    return cartInfoList;
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                //log ex
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// Get Card Info List
        /// </summary>
        /// <param name="cartIds"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public static List<DbCartInfo> GetCartInfo(List<int> cartIds)
        {
            try
            {
                using (BookStoreEntities context = new BookStoreEntities())
                {
                    List<DbCartInfo> cartInfoList = context.Carts
                        .Where(W => cartIds.Contains(W.Id))
                        .Join(context.Books, c => c.BookId, b => b.Id, (c, b) => new { c, b })
                        .Select(S => new DbCartInfo()
                        {
                            Id = S.c.Id,
                            BookId = S.c.BookId,
                            Title = S.b.Title,
                            Description = S.b.Description,
                            Author = S.b.Author,
                            ImageUrl = S.b.ImageUrl,
                            Qty = S.c.Qty,
                            Price = S.b.Price,
                        }).ToList();

                    return cartInfoList;
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                //log ex
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// RemoveCartInfo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cartIds"></param>
        private static void RemoveCartInfo(BookStoreEntities context, List<int> cartIds)
        {
            try
            {
                List<Cart> carts = context.Carts.Where(W => cartIds.Contains(W.Id)).ToList();

                context.Carts.RemoveRange(carts);

                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Insert order details
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dbCartInfoList"></param>
        /// <param name="orderId"></param>
        private static void InsertOrderDetailInfo(BookStoreEntities context, List<DbCartInfo> dbCartInfoList, int orderId)
        {
            try
            {

                foreach (DbCartInfo cart in dbCartInfoList)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = orderId;
                    orderDetail.BookId = cart.BookId;
                    orderDetail.Qty = cart.Qty;
                    orderDetail.Price = cart.Price;
                    orderDetail.TotalAmount = cart.Qty * cart.Price;

                    context.OrderDetails.Add(orderDetail);
                }

                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Insert Order
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dbCartInfoList"></param>
        /// <param name="userId"></param>
        /// <param name="totalAmount"></param>
        /// <returns></returns>
        private static int InsertOrderInfo(BookStoreEntities context, List<DbCartInfo> dbCartInfoList, int userId, double totalAmount)
        {
            try
            {
                // order info 
                DateTime orderDate = DateTime.Now;
                byte orderStatus = (byte)eOrderStatus.Ordered;
                byte paymentStatus = (byte)ePaymentStatus.Paid;

                Order order = new Order();
                order.OrderDate = orderDate;
                order.OrderStatus = orderStatus;
                order.TotalAmount = totalAmount;
                order.PaymentStatus = paymentStatus;
                order.UserId = userId;

                context.Orders.Add(order);
                context.SaveChanges();

                return order.OrderId;

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
