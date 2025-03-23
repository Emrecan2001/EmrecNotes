using System.ComponentModel.DataAnnotations;

namespace EmrecNotes.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(50)]
        public string UserName { get; set; }

        public string? UserRole { get; set; }

        [Required]
        public string PasswordHashed { get; set; }
    }
}
