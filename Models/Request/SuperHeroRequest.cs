﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.Models.Request
{
    [DisplayName("aaaaaaaaa")]
    public class SuperHeroRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
    }
}