using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDataAccess.Models.CartModels.Request
{
    public class ReqDBAddCartInfo
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public int Qty { get; set; }

        public byte Option { get; set; }
    }
}
