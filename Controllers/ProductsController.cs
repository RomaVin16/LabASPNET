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

        [HttpGet("EditProduct/{id}")]
        public IActionResult EditProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product); 
        }

        [HttpPost("EditProduct/{id}")]
        public IActionResult EditProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            var dbProduct = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (dbProduct == null)
            {
                return NotFound();
            }

            dbProduct.Name = product.Name;
            dbProduct.Price = product.Price;
            dbProduct.Description = product.Description;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            var orders = _context.Orders.Where(o => o.ProductId == id).ToList();
            _context.Orders.RemoveRange(orders);

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
