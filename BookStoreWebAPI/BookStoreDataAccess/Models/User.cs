using System;
using System.Collections.Generic;

namespace BookStoreDataAccess.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserDlpName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime? RegistDateTime { get; set; }

    public DateTime? LastPasswordUpdateDateTime { get; set; }

    public int? DelFlg { get; set; }

    public DateTime? DeleteDateTime { get; set; }

    public int? DeleteUserId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<RolesUser> RolesUsers { get; set; } = new List<RolesUser>();
}
