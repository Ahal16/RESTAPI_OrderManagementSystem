using System;
using System.Collections.Generic;

namespace OrderManagementDemo.Model;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerNumber { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();
}
