using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolChoicePlayground.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string name { get; set; }
        [Phone]
        public long phoneNum { get; set; }
        public string email { get; set; }
        public List<School> userSchools { get; set; }
        public bool alerts { get; set; }
    }
}