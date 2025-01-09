using LabASPNET.Models;
using LabASPNET.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LabASPNET.Controllers
{
    public class ProductsController : Controller
    {
        private readonly RepositoryContext _context;

        public ProductsController(RepositoryContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var products = _context.Products.ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost("EditProduct")]
        public IActionResult EditProduct(List<Product> products)
        {
var dbProducts = _context.Products.ToList();

for (var i = 0; i < products.Count; i++)
{
	var product = products[i];
    var dbProduct = dbProducts[i];

    dbProduct.Name = product.Name;
    dbProduct.Price = product.Price;
    dbProduct.Description = product.Description;
}

_context.SaveChanges();

return RedirectToAction("Index");
        }

		[HttpPost("DeleteProduct/{id}")]
		public IActionResult DeleteProduct(int id)
		{
			var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

			if (product == null)
			{
				return NotFound(); 
			}

			var orders = _context.Orders.Where(o => o.ProductId == id).ToList();

			if (orders.Any())
			{
				_context.Orders.RemoveRange(orders);
			}

			_context.Products.Remove(product); 
			_context.SaveChanges(); 

			return RedirectToAction("Index");
		}

	}
}
