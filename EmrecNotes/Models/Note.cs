﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmrecNotes.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(100000)] // contains max 100.000 letters
        public string? Content { get; set; }

        // foreign key
        public int UserId { get; set; }

        // Navigation Property
        [ForeignKey("UserId")]
        public Account User { get; set; }
    }
}
