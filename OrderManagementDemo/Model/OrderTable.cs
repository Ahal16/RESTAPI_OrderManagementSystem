using System;
using System.Collections.Generic;

namespace OrderManagementDemo.Model;

public partial class OrderTable
{
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? CustomerId { get; set; }

    public int? OrderItemId { get; set; }
    public virtual Customer? Customer { get; set; }

    public virtual OrderItem? OrderItem { get; set; }
}
