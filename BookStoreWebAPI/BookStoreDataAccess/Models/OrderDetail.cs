using System;
using System.Collections.Generic;

namespace BookStoreDataAccess.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int BookId { get; set; }

    public int Qty { get; set; }

    public double Price { get; set; }

    public double TotalAmount { get; set; }

    public virtual Book Book { get; set; }

    public virtual Order Order { get; set; }
}
