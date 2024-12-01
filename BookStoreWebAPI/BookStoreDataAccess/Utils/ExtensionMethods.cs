using BookStoreDataAccess.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDataAccess.Utils
{
    public static class ExtensionMethods
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string orderByProperty, bool desc)
        {
            var entityType = typeof(T);
            var propertyInfo = entityType.GetProperty(orderByProperty);

            if (propertyInfo == null)
            {
                throw new ArgumentException($"No property '{orderByProperty}' found on type '{entityType.Name}'");
            }

            var parameter = Expression.Parameter(entityType, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            var resultExpression = Expression.Call(
                typeof(Queryable),
                desc ? "OrderByDescending" : "OrderBy",
                new Type[] { entityType, propertyInfo.PropertyType },
                source.Expression,
                Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExpression);
        }

        internal static StringBuilder OrderBySkipTake(this StringBuilder source, DbFilterInfo filterInfo, bool isDesc)
        {
            bool bContinue = false;

            if (filterInfo == null)
            {
                return source;
            }

            if (filterInfo != null)
            {
                bContinue = true;
            }

            string sOrder = " ASC ";

            if (bContinue && isDesc)
            {
                sOrder = " DESC ";
            }

            string sPropertyName = filterInfo.SortColumn;

            if (bContinue && !string.IsNullOrWhiteSpace(filterInfo.SortColumn))
            {
                sPropertyName = filterInfo.SortColumn;
            }

            if (bContinue)
            {
                string sOrderBySelect = string.Format("SELECT row_number() OVER (ORDER BY {0} {1}) AS row_number, * FROM (", sPropertyName, sOrder);

                source.Insert(0, sOrderBySelect);
                source.Append(") AS myData ");
                source.Insert(0, string.Format("SELECT TOP({0}) * FROM (", filterInfo.Length));
                source.Append(string.Format(") AS myData WHERE myData.row_number > {0} ", filterInfo.Start));
                source.Append(string.Format(" ORDER BY {0} {1}", sPropertyName, sOrder));
            }

            return source;
        }
    }
}
