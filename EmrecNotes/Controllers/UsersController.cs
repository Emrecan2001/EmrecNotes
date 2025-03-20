using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EmrecNotes.Controllers
{
    [Route("u")]
    [Authorize] //Only Authorizing users can access this controller otherwise they are returning back to LoginPath
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            // pull cookies (demo)
            var userName = User.Identity.Name;
            Console.WriteLine($"Welcome {userName}");
            return View();
        }
    }
}
