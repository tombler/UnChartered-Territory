using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolChoicePlayground.Models
{
    public class MyUser
    {
        [Key]
        public int UserId { get; set; }
        public string name { get; set; }     
        public string phoneNum { get; set; }
        public string email { get; set; }
        public List<School> userSchools { get; set; }
        public bool alerts { get; set; }
        [Required]
        public virtual string AspUser { get; set; }

        public MyUser() {
            userSchools = new List<School>();
        }

    }
}