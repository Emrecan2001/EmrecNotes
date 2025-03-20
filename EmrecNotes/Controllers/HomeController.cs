using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using EmrecNotes.Data;
using EmrecNotes.Models;
using BCrypt.Net;

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

            // check if user exist or if the password is matching (nonhashed password, hashedpassword) / it's going to turn hash and check
            if (account == null || !BCrypt.Net.BCrypt.Verify(user.PasswordHashed, account.PasswordHashed))
            {   
                Console.WriteLine("no account or wrong password");
                return View();
            }

            // claims include users desired informations
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim(ClaimTypes.Role, "Member")
            };

            // creating Identity
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Authentication Properties
            var authProperties = new AuthenticationProperties
            {
                // when it's empty it uses default settings
            };

            // Login in with claim Identity
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Users");

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Id,Email,UserName,PasswordHashed")]Account NewUser)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("User invalid");
                return View();
            }

            // this returns true or false 
            if (await _context.Account.AnyAsync(user => user.Email == NewUser.Email)){
                Console.WriteLine("This email is already used");
                return View();
            };

            // hashing password
            NewUser.PasswordHashed = BCrypt.Net.BCrypt.HashPassword(NewUser.PasswordHashed);

            await _context.Account.AddAsync(NewUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
            
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
