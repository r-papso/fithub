﻿using System.ComponentModel.DataAnnotations;

namespace Fithub.UI.Models
{
    public class Credentials
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
