using Microsoft.AspNetCore.Mvc;
using OrderManagementDemo.Model;
using OrderManagementDemo.Repository;
using OrderManagementDemo.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderManagementDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        //DI - Dependency Injection
        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        #region 1 - Get all Orders - search all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderTable>>> GetAllOrderss()
        {
            var orders = await _repository.GetTblOrder();
            if (orders == null)
            {
                return NotFound("No employees found");
            }
            return Ok(orders);
        }
        #endregion

        #region 2 - Get all from viewModel 
        [HttpGet("vm")]
        public async Task<ActionResult<IEnumerable<CustOrderViewModel>>> GetAllEmployeesByViewModel()
        {
            var orders = await _repository.GetOrderViewModel();
            if (orders == null)
            {
                return NotFound("No orders found");
            }
            return Ok(orders);
        }

        #endregion

        #region 3 - Get Orders - Search By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderTable>> GetCustOrdersById(int id)
        {
            var order = await _repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound("No orders found");
            }
            return Ok(order);
        }

        #endregion

        #region   4  - Insert an order -return order record
        public async Task<ActionResult<OrderTable>> InsertTblOrdersReturnRecord(OrderTable orderTable)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee
                var neworder = await _repository.PostTblOrderReturnRecord(orderTable);
                if (neworder != null)
                {
                    return Ok(neworder);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region    5 - Insert an order -return Id

        [HttpPost("v1")]
        public async Task<ActionResult<int>> InsertTblOrdersReturnId(OrderTable orderTable)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee
                var newOrderId = await _repository.PostTblOrderReturnId(orderTable);
                if (newOrderId != null)
                {
                    return Ok(newOrderId);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region    6  - Update an order with ID
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateTblOrdersReturnRecord(int id, OrderTable orderTable)
        {
            if (ModelState.IsValid)
            {
                var updateOrderTable = await _repository.PutTblOrder(id, orderTable);
                if (updateOrderTable != null)
                {
                    return Ok(updateOrderTable);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region  7  - Delete an Order
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                var result = _repository.DeleteTblOrder(id);

                if (result == null)
                {
                    //if result indicates failure or null
                    return NotFound(new
                    {
                        success = false,
                        message = "Order could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }
        #endregion

        #region   8 - Get all OrderItems - search all
        [HttpGet("v2")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetAllTblOrderItems()
        {
            var ots = await _repository.GetTblOrderItems();
            if (ots == null)
            {
                return NotFound("No OrderItems found");
            }
            return Ok(ots);
        }
        #endregion

        #region   9 - Get all Customers - search all
        [HttpGet("v3")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllTblCustomers()
        {
            var ots = await _repository.GetAllCustomers();
            if (ots == null)
            {
                return NotFound("No Customers found");
            }
            return Ok(ots);
        }
        #endregion

























        //// GET: api/<OrderController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<OrderController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<OrderController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<OrderController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<OrderController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
