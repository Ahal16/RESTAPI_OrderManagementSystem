using System;
using System.Collections.Generic;

namespace OrderManagementDemo.Model;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitePrice { get; set; }

    public int? ItemId { get; set; }

    public virtual Item? Item { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();
}
