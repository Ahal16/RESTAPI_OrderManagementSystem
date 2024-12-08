using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementDemo.Model;
using OrderManagementDemo.ViewModel;

namespace OrderManagementDemo.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderMgntDbContext _context;

        public OrderRepository(OrderMgntDbContext context)
        {
            _context = context;
        }

        #region 1 - Get all Orders
        public async Task<ActionResult<IEnumerable<OrderTable>>> GetTblOrder()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.OrderTables.Include(order => order.Customer).Include(order => order.OrderItem).ToListAsync();
                }
                
                return new List<OrderTable>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 2 - Order ViewModel
        public async Task<ActionResult<IEnumerable<CustOrderViewModel>>> GetOrderViewModel()
        {
            //LINQ
            try
            {
                if (_context != null)
                {
                    //LINQ
                    return await (from o in _context.OrderTables
                                  join c in _context.Customers on o.CustomerId equals c.CustomerId
                                  join oi in _context.OrderItems on o.OrderItemId equals oi.OrderItemId
                                  join i in _context.Items on oi.ItemId equals i.ItemId
                                  select new CustOrderViewModel
                                  {
                                      CustomerId = c.CustomerId,
                                      CustomerName = c.CustomerName,
                                      ItemName = i.ItemName,
                                      Price = i.Price,
                                      Quantity = oi.Quantity,
                                      OrderDate = o.OrderDate
                                  }).ToListAsync();

                }
              
                return new List<CustOrderViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 3 - Get an order by its Id
        public async Task<ActionResult<OrderTable>> GetOrderById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var custOrder = await _context.OrderTables.Include(order => order.Customer).Include(order => order.OrderItem).FirstOrDefaultAsync(ord => ord.OrderId == id);
                    return custOrder;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   4  - Insert an order -return order record
        public async Task<ActionResult<OrderTable>> PostTblOrderReturnRecord(OrderTable ordertable)
        {
            try
            {
                if (ordertable == null)
                {
                    throw new ArgumentException(nameof(ordertable), "Order data is null");
                   
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                await _context.OrderTables.AddAsync(ordertable);
                await _context.SaveChangesAsync();
                var OrderCustOI = await _context.OrderTables.Include(ord => ord.Customer).Include(ord => ord.OrderItem)
                    .FirstOrDefaultAsync(ord => ord.OrderId == ordertable.OrderId);

                return OrderCustOI;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   5 - Insert an order -return Id
        public async Task<ActionResult<int>> PostTblOrderReturnId(OrderTable ordertable)
        {
            try
            {
                if (ordertable == null)
                {
                    throw new ArgumentException(nameof(ordertable), "Order data is null");
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                await _context.OrderTables.AddAsync(ordertable);
                var changesRecord = await _context.SaveChangesAsync();
                if (changesRecord > 0)
                {
                    return ordertable.OrderId;
                }
                else
                {
                    throw new Exception("Failed to save Order record to the database");
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   6  - Update an order with ID
        public async Task<ActionResult<OrderTable>> PutTblOrder(int id, OrderTable orderTable)
        {
            try
            {
                if (orderTable == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                var existingOrder = await _context.OrderTables.FindAsync(id);
                if (existingOrder == null)
                {
                    return null;
                }

                existingOrder.OrderDate = orderTable.OrderDate;
                existingOrder.CustomerId = orderTable.CustomerId;
                existingOrder.OrderItemId = orderTable.OrderItemId;

                await _context.SaveChangesAsync();

                var OrderCustomerOI = await _context.OrderTables.Include(ord => ord.Customer).Include(ord => ord.OrderItem)
                    .FirstOrDefaultAsync(existingOrder => existingOrder.OrderId == orderTable.OrderId);

                return OrderCustomerOI;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   7  - Delete an order
        public JsonResult DeleteTblOrder(int id)
        {
            try
            {
                if (id <= null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid Order Id"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database context is not initialized"
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
                var existingOrder = _context.OrderTables.Find(id);

                if (existingOrder == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Order not found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
               
                _context.OrderTables.Remove(existingOrder);

                _context.SaveChangesAsync();
                return new JsonResult(new
                {
                    success = true,
                    message = "Order Deleted successfully"
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Database context is not initialized"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        #endregion

        #region  8  - Get all order
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetTblOrderItems()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.OrderItems.ToListAsync();
                }
           
                return new List<OrderItem>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  9  - Get all Customer
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Customers.ToListAsync();
                }
                
                return new List<Customer>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
