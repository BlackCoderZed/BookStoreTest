using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BookStoreDataAccess.DAO
{
    public class BaseDao
    {
        protected static TransactionScope GetReadCommitmentTransactionScope()
        {
            TransactionOptions options = new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadUncommitted,
                Timeout = TimeSpan.FromMinutes(1)
            };


            TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, options);
            return transaction;
        }
    }
}
