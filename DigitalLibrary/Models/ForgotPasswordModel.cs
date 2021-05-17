﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress, Display(Name = "Please enter email address")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
