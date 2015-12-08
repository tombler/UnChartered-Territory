using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolChoicePlayground.Models
{
    public class School : IComparable
    {
        public enum schoolLevel { Elementary, Middle, High };
        public enum State {[Description("Tennessee")] TN };
        public enum schoolType { Charter };

        [Key]
        public int SchoolId { get; set; }
        [Required]
        public string name { get; set; }
        public string grades { get; set; }
        public Address address { get; set; }
        [Phone]
        public string phoneNum { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        [MaxLength(200)]
        public string addlInfo { get; set; }
        public List<SchoolAppUser> Users { get; set; }
        [Required]
        public schoolLevel level { get; set; }
        public schoolType type { get; set; }
        public string website { get; set; }

        public int CompareTo(object obj)
        {
            // Compare Schools by name
            School other_school = obj as School;
            int answer = this.name.CompareTo(other_school.name);
            return answer;
        }
    }
}