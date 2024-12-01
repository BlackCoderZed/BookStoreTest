using System;
using System.Collections.Generic;

namespace BookStoreDataAccess.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public byte OrderStatus { get; set; }

    public double TotalAmount { get; set; }

    public double? DiscountAmount { get; set; }

    public double? TaxAmount { get; set; }

    public byte PaymentStatus { get; set; }

    public DateTime? CompleteDateTime { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User User { get; set; }
}
