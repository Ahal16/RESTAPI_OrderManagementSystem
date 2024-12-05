using OrderManagementDemo.Model;

namespace OrderManagementDemo.ViewModel
{
    public class CustOrderViewModel
    {
        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public string? ItemName { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public DateTime? OrderDate { get; set; }
    }
}
