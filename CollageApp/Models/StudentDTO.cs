﻿using System.ComponentModel.DataAnnotations;

namespace CollageApp.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required]
        public string StudentName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
