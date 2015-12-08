﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolChoicePlayground.Models
{
    public class SchoolAppUser
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string name { get; set; }
        [Phone]
        public string phoneNum { get; set; }
        public string provider { get; set; } // For sending texts via email if alerts are on
        public string email { get; set; }
        public List<School> userSchools { get; set; }
        public bool alerts { get; set; }
    }
}