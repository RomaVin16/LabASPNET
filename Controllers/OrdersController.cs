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
		        if (order.OrderDate.Kind == DateTimeKind.Unspecified)
		        {
			        order.OrderDate = DateTime.SpecifyKind(order.OrderDate, DateTimeKind.Utc);
		        }
		        else if (order.OrderDate.Kind == DateTimeKind.Local)
		        {
			        order.OrderDate = order.OrderDate.ToUniversalTime();
		        }

		        var user = _context.Users.Find(order.UserId);

		        if (user == null)
		        {
					return View(order);
		        }

		        var product = _context.Products.Find(order.ProductId);

		        if (product == null)
		        {
					return View(order);
		        }

		        _context.Orders.Add(order);
		        _context.SaveChanges();

		        return RedirectToAction("Index");
	        }

	        return View(order);
        }


		[HttpPost("EditOrder")]
        public IActionResult EditOrder(List<Order> orders)
        {
            var dbOrders = _context.Orders.ToList();

            for (var i = 0; i < orders.Count; i++)
            {
	            var order = orders[i];
                var dbOrder = dbOrders[i];

                dbOrder.UserId = order.UserId;
                dbOrder.ProductId = order.ProductId;
                dbOrder.Quantity = order.Quantity;

                if (order.OrderDate.Kind == DateTimeKind.Unspecified)
                {
	                dbOrder.OrderDate = DateTime.SpecifyKind(order.OrderDate, DateTimeKind.Utc);
                }
                else if (order.OrderDate.Kind == DateTimeKind.Local)
                {
	                dbOrder.OrderDate = order.OrderDate.ToUniversalTime();
                }
			}

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost("DeleteOrder/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);

            if (order == null) return NotFound();
            
            _context.Orders.Remove(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
