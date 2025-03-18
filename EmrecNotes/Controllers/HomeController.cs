using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmrecNotes.Data;
using EmrecNotes.Models;

namespace EmrecNotes.Controllers
{
    public class HomeController : Controller
    {

        private readonly EmrecNotesContext _context;
        public HomeController(EmrecNotesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Account user)
        {
            var account = await _context.Account.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (account == null)
            {
                Console.WriteLine("Account not found!!"); // failed to join
                return View();
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Id,Email,UserName,PasswordHashed")]Account NewUser)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("User valid");
                return View();
            }
            Console.WriteLine("User invalid");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
