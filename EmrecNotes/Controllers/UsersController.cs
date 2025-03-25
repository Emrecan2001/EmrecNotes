using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmrecNotes.Data;
using EmrecNotes.Models;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            // get user id cookie as integer
            var UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            Console.WriteLine(UserId.GetType());

            // list all data from note table with specific user id
            var Notes = await _context.Note.Where(n => n.UserId == UserId).ToListAsync();

            return View(Notes);
        }

        [Route("create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create(Note note)
        {

            var UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            note.UserId = UserId;

            await _context.Note.AddAsync(note);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Route("edit/{Id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            // return note with specific Id
            return View(await _context.Note.FirstOrDefaultAsync(n => n.Id == Id));
        }

        // take a break and continue
        [Route("edit/{Id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, Note UpdatedNote)
        {

            var Note = await _context.Note.FirstOrDefaultAsync(n => n.Id == Id);

            Console.WriteLine(Id);
            Console.WriteLine(UpdatedNote.Title);
            Console.WriteLine(UpdatedNote.Content);

            Note.Title = UpdatedNote.Title;
            Note.Content = UpdatedNote.Content;

            _context.Update(Note);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
