using LabASPNET.Models;
using LabASPNET.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LabASPNET.Controllers
{
    public class OrdersController: Controller
    {
        private readonly RepositoryContext _context;

        public OrdersController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();

            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(order);
        }

        [HttpPost("EditTable")]
        public IActionResult EditTable(Dictionary<int, Order> orders)
        {
            if (orders == null || orders.Count == 0)
            {
                return BadRequest("No orders to edit");
            }

            var dbOrders = _context.Orders.ToList();
            foreach (var orderEntry in orders)
            {
                var id = orderEntry.Key;
                var order = orderEntry.Value;

                var dbOrder = dbOrders.FirstOrDefault(o => o.OrderId == id);
                if (dbOrder == null)
                {
                    ModelState.AddModelError($"order", $"Can not find order with Id: {order.OrderId}");
                    continue;
                }

                dbOrder.UserId = order.UserId;
                dbOrder.ProductId = order.ProductId;
                dbOrder.Quantity = order.Quantity;
                dbOrder.OrderDate = order.OrderDate;

                if (order.OrderDate.Kind == DateTimeKind.Unspecified)
                {
                    dbOrder.OrderDate = DateTime.SpecifyKind(order.OrderDate, DateTimeKind.Utc);
                }
                else if (order.OrderDate.Kind == DateTimeKind.Local)
                {
                    dbOrder.OrderDate = order.OrderDate.ToUniversalTime();
                }
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("DeleteOrder/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
