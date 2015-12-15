using Newtonsoft.Json;
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
        [Required]
        public virtual Address address { get; set; }
        [JsonProperty("phone")]
        public string phoneNum { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        [MaxLength(200)]
        public string addlInfo { get; set; }
        public virtual List<MyUser> MyUsers { get; set; }
        [Required]
        [JsonProperty("schoolLevel")]
        public schoolLevel level { get; set; }
        [JsonProperty("schoolType")]
        public schoolType type { get; set; }
        [JsonProperty("link")]
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