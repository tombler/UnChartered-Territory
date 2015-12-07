using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolChoicePlayground.Models
{
    public class School
    {
        public enum schoolLevel { Elementary, Middle, High };
        public enum State
        {
            [Description("Tennessee")]
            TN
        }

        [Key]
        public int SchoolId { get; set; }
        [Required]
        public string name { get; set; }
        public string grades { get; set; }
        public Address address { get; set; }
        [Phone]
        public long phoneNum { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        [MaxLength(200)]
        public string addlInfo { get; set; }
        public List<User> Users { get; set; }
        [Required]
        public schoolLevel level { get; set; } 
        public string socialMedia { get; set; }
    }
}