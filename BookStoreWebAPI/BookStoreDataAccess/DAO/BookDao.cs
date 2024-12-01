using BookStoreDataAccess.Models;
using BookStoreDataAccess.Models.BookModels.Response;
using BookStoreDataAccess.Models.CartModels;
using BookStoreDataAccess.Models.Common;
using BookStoreDataAccess.Utils;
using CommonExtension.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDataAccess.DAO
{
    public class BookDao : BaseDao
    {
        /// <summary>
        /// CheckBalance
        /// </summary>
        /// <param name="dbCartInfoList"></param>
        /// <exception cref="AppException"></exception>
        public static void CheckBalance(List<DbCartInfo> dbCartInfoList)
        {
            try
            {
                using (BookStoreEntities context = new BookStoreEntities())
                {
                    List<Book> books = context.Books
                        .Where(W => dbCartInfoList.Select(S => S.BookId).ToList().Contains(W.Id))
                        .Where(W => W.DelFlg == null).ToList();

                    if (books == null)
                    {
                        throw new AppException(string.Format(AppException.MSG_NOT_EXIST, typeof(Book).Name));
                    }

                    List<Book> notEnoughList = new List<Book>();

                    foreach (Book book in books)
                    {
                        int reqQty = dbCartInfoList.Where(W => W.BookId == book.Id).Select(S => S.Qty).FirstOrDefault();

                        if (reqQty > book.Qty)
                        {
                            notEnoughList.Add(book);
                        }
                    }

                    if (notEnoughList.Count > 0)
                    {
                        string notEnoughIdStr = string.Join(",", notEnoughList.Select(S => S.Id).ToArray());

                        throw new AppException(string.Format(AppException.MSG_NOT_ENOUGH_WITH_ID, notEnoughIdStr));
                    }
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                // log ex
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// CheckExist
        /// </summary>
        /// <param name="bookId"></param>
        /// <exception cref="AppException"></exception>
        public static void CheckExist(int bookId)
        {
            try
            {
                using (BookStoreEntities context = new BookStoreEntities())
                {
                    Book book = context.Books
                        .Where(W => W.Id == bookId)
                        .Where(W => W.DelFlg == null).FirstOrDefault();

                    if (book == null)
                    {
                        throw new AppException(string.Format(AppException.MSG_NOT_EXIST, typeof(Book).Name));
                    }
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                // log ex
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// CheckRequestQty
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="reqQty"></param>
        /// <exception cref="AppException"></exception>
        public static void CheckRequestQty(int bookId, int reqQty)
        {
            try
            {
                using (BookStoreEntities context = new BookStoreEntities())
                {
                    Book book = context.Books.Where(W => W.Id.Equals(bookId)).FirstOrDefault();

                    if (book == null )
                    {
                        throw new AppException(string.Format(AppException.MSG_NOT_EXIST, typeof(Book).Name));
                    }

                    if (book.Qty < reqQty)
                    {
                        throw new AppException(AppException.MSG_NOT_ENOUGH);
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
        /// GetBookInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public static DbBookInfo GetBookInfo(int id)
        {
            try
            {
                using (BookStoreEntities context = new BookStoreEntities())
                {
                    DbBookInfo book = context.Books
                        .Join(context.Categories, b => b.CategoryId, c => c.Id, (b, c) => new { b, c })
                        .Where(W => W.b.Id == id)
                        .Select(S => new DbBookInfo()
                        {
                            Id = S.b.Id,
                            Title = S.b.Title,
                            Author = S.b.Author,
                            ReleaseDate = S.b.ReleaseDate,
                            Description = S.b.Description,
                            Category = S.c.CategoryName,
                            Price = S.b.Price,
                            Qty = S.b.Qty,
                            ImageUrl = S.b.ImageUrl
                        }).FirstOrDefault();

                    if (book == null)
                    {
                        throw new AppException(string.Format(AppException.MSG_NOT_EXIST, typeof(Book).Name));
                    }

                    return book;
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                // log ex
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// GetBookInfoList
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public static ResDbBookInfoList GetBookInfoList(DbFilterInfo filter)
        {
            ResDbBookInfoList result = new ResDbBookInfoList();

            try
            {
                using (BookStoreEntities context = new BookStoreEntities())
                {
                    IQueryable<DbBookInfo> query = context.Books
                        .Join(context.Categories, b => b.CategoryId, c => c.Id, (b, c) => new {b, c})
                        .Select(S => new DbBookInfo()
                        {
                            Id = S.b.Id,
                            Title = S.b.Title,
                            Author = S.b.Author,
                            ReleaseDate = S.b.ReleaseDate,
                            Description = S.b.Description,
                            Category = S.c.CategoryName,
                            Price = S.b.Price,
                            Qty = S.b.Qty,
                            ImageUrl = S.b.ImageUrl
                        }).AsQueryable();

                    result.TotalRecord = query.Count();

                    if (filter != null)
                    {
                        query = ApplyFilter(query, filter, out int totalCount);
                        result.TotalRecord = totalCount;
                    }

                    result.BookList = query.ToList();
                    return result;
                }
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception)
            {
                // log ex
                throw new AppException(AppException.MSG_READ_FAIL);
            }
        }

        /// <summary>
        /// UpdateQty
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dbCartInfoList"></param>
        internal static void UpdateQty(BookStoreEntities context, List<DbCartInfo> dbCartInfoList)
        {
            try
            {
                List<int> bookIds = dbCartInfoList.Select(S => S.BookId).ToList();

                List<Book> bookList = context.Books
                    .Where(W => bookIds.Contains(W.Id))
                    .ToList();

                for (int i = 0; i < bookList.Count; i++)
                {
                    int id = bookList[i].Id;
                    int qty = dbCartInfoList.Where(W => W.BookId == id).Select(S => S.Qty).FirstOrDefault();
                    bookList[i].Qty = bookList[i].Qty - qty > 0 ? bookList[i].Qty - qty : 0;
                }

                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Filter
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        private static IQueryable<DbBookInfo> ApplyFilter(IQueryable<DbBookInfo> query, DbFilterInfo filter, out int totalRecord)
        {
            try
            {
                totalRecord = query.Count();

                if  (filter == null)
                {
                    return query;
                }

                if (!string.IsNullOrEmpty(filter.SearchValue) && !string.IsNullOrWhiteSpace(filter.SearchValue))
                {
                    query = query.Where(W => W.Title.Contains(filter.SearchValue)
                    || W.Author.Contains(filter.SearchValue)
                    || W.Description.Contains(filter.SearchValue)
                    || W.Category.Contains(filter.SearchValue));
                }

                totalRecord = query.Count();
                // order
                bool isDesc = filter.SortOrder == "desc";   

                query = query.OrderByDynamic(filter.SortColumn, isDesc);

                // skip take
                query = query.Skip(filter.Start).Take(filter.Length);

                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
