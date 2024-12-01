using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonExtension.Utils
{
    public enum eCartOption
    {
        Add,
        Remove
    }

    public enum eOrderStatus
    {
        Pending,
        Ordered,
        OnTheWay,
        Delivered,
        Completed
    }

    public enum ePaymentStatus
    {
        NotPaid,
        Paid
    }
}
