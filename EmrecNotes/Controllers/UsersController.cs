using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmrecNotes.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmrecNotes.Controllers
{
    [Route("u")]
    [Authorize] //Only Authorizing users can access this controller otherwise they are returning back to LoginPath
    public class UsersController : Controller
    {

        private readonly EmrecNotesContext _context;

        public UsersController(EmrecNotesContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            // get user id cookie as integer
            var UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            Console.WriteLine(UserId.GetType());

            // list all data from note table with specific user id
            var Notes = await _context.Note.Where(n => n.UserId == UserId).ToListAsync();

            return View(Notes);
        }
    }
}
