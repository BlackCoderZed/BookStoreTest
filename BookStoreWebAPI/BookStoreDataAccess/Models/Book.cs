using System;
using System.Collections.Generic;

namespace BookStoreDataAccess.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public int CategoryId { get; set; }

    public string Author { get; set; }

    public string Description { get; set; }

    public int Qty { get; set; }

    public double Price { get; set; }

    public int? DelFlg { get; set; }

    public int? DeleteUserId { get; set; }

    public string ImageUrl { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
