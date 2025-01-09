using LabASPNET.Models;
using LabASPNET.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LabASPNET.Controllers
{
    [Route("Users")] // Базовый маршрут для контроллера Users
    public class UsersController : Controller
    {
        private readonly RepositoryContext _context;

        public UsersController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpGet("EditTable")]
        public IActionResult EditTable()
        {
            var users = _context.Users.ToList();

            return View(users);
        }

        [HttpPost("EditTable")]
        public IActionResult EditTable(List<User> users)
        {

            var dbUsers = _context.Users.ToList();

            for (var i = 0; i < users.Count; i++)
            {
                var user = users[i];
                var dbUser = dbUsers[i]; 

                dbUser.Email = user.Email;
                dbUser.Name = user.Name;
                dbUser.RegistrationDate = user.RegistrationDate;


                if (user.RegistrationDate.Kind == DateTimeKind.Unspecified)
                {
                    dbUser.RegistrationDate = DateTime.SpecifyKind(user.RegistrationDate, DateTimeKind.Utc);
                }
                else if (user.RegistrationDate.Kind == DateTimeKind.Local)
                {
                    dbUser.RegistrationDate = user.RegistrationDate.ToUniversalTime();
                }

            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);

                var orders = _context.Orders.Where(o => o.UserId == id).ToList();
                _context.Orders.RemoveRange(orders);

                _context.Users.Remove(user);
                _context.SaveChanges();
            

            return RedirectToAction("Index"); 
        }
    }
}