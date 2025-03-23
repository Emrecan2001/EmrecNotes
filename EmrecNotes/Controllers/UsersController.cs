using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EmrecNotes.Controllers
{
    [Route("u")]
    [Authorize] //Only Authorizing users can access this controller otherwise they are returning back to LoginPath
    public class UsersController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            // pull cookies (demo)
            var userName = User.Identity.Name;

            return View("Index", userName);
        }
    }
}
