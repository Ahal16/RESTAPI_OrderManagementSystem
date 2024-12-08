using Microsoft.AspNetCore.Mvc;
using OrderManagementDemo.Model;
using OrderManagementDemo.ViewModel;

namespace OrderManagementDemo.Repository
{
    public interface IOrderRepository
    {
        #region   1  - Get all orders from DB - Search All
        public Task<ActionResult<IEnumerable<OrderTable>>> GetTblOrder();
        #endregion

        #region  2  - Get all orders using ViewModel
        public Task<ActionResult<IEnumerable<CustOrderViewModel>>> GetOrderViewModel();
        #endregion

        #region 3 - Get an order based on Id
        public Task<ActionResult<OrderTable>> GetOrderById(int id);
        #endregion

        #region  4  - Insert an order -return order record
        public Task<ActionResult<OrderTable>> PostTblOrderReturnRecord(OrderTable ordertable);
        #endregion

        #region    5 - Insert an order -return Id
        public Task<ActionResult<int>> PostTblOrderReturnId(OrderTable ordertable);
        #endregion

        #region  6  - Update an order with ID 
        public Task<ActionResult<OrderTable>> PutTblOrder(int id, OrderTable orderTable);
        #endregion

        #region 7  - Delete an order
        public JsonResult DeleteTblOrder(int id); 
        #endregion

        #region 8  - Get all orderItems
        public Task<ActionResult<IEnumerable<OrderItem>>> GetTblOrderItems();
        #endregion

        #region  9  - Get all Customer
        public Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers();
        #endregion
    }
}
