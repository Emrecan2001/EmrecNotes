using Microsoft.EntityFrameworkCore;

namespace EmrecNotes.Data
{
    public class EmrecNotesContext : DbContext
    {
        public EmrecNotesContext(DbContextOptions<EmrecNotesContext> Options)
        :base(Options)
        {
        }

        public DbSet<EmrecNotes.Models.Account> Account { get; set; }
        public DbSet<EmrecNotes.Models.Note> Note { get; set; }

        //connect Account model into Account table in database
    }
}
