using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDataAccess.Utils
{
    public class DataAccessUtil
    {
        private static string _CONNECTION_STRING = null;

        public DataAccessUtil(string connectionString)
        {
            _CONNECTION_STRING = connectionString;
        }

        public static string GetConnectionString()
        {
            return _CONNECTION_STRING;
        }
    }
}
