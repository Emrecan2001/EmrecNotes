using Microsoft.EntityFrameworkCore;

namespace EmrecNotes.Data
{
    public class EmrecNotesContext : DbContext
    {
        public EmrecNotesContext(DbContextOptions<EmrecNotesContext> Options)
        :base(Options)
        {
        }

        public DbSet<EmrecNotes.Models.Account> Accounts { get; set; }
    }
}
