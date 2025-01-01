using CodeShares.Data;
using CodeShares.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeShares.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        private const string AdminPassword = "";

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "AnaSayfa";
            return View(_context.Codes.OrderByDescending(x => x.Created).ToList());
        }
        public IActionResult Create()
        {
            ViewData["Title"] = "KodKayıt";
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Add(Code model)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid) 
            {
                _context.Codes.Add(model);
                _context.SaveChanges();
                TempData["mesaj"] = "Yeni kod eklenmiştir";
            }
            return RedirectToAction("index");
        }
        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(string password)
        {
            if (password == AdminPassword)
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                return RedirectToAction("Index");
            }

            TempData["error"] = "Kod için Barış ile görüş!";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsLoggedIn");
            return RedirectToAction("Index", "Home");
        }
        private bool IsUserLoggedIn()
        {
            return HttpContext.Session.GetString("IsLoggedIn") == "true";
        }
    }
}
