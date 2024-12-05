using System;
using System.Collections.Generic;

namespace OrderManagementDemo.Model;

public partial class Item
{
    public int ItemId { get; set; }

    public string? ItemName { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
