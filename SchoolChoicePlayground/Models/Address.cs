using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SchoolChoicePlayground.Models
{
    public class Address
    {
        public enum State
        {
            [Description("Tennessee")]
            TN
        }

        [Key]
        public int AddressId { get; set; }
        [JsonProperty("addressLine1")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string city { get; set; }
        public State state { get; set; }
        [RegularExpression(@"[\d]{5}")]
        public string zip { get; set; }
        public School school { get; set; }

        public void Validate()
        {
            string zipPattern = @"^\d{5}(?:[-\s]\d{4})?$";
            string cityPattern = @"^[A-Z]{1}[a-z]+";
            if (!Regex.IsMatch(zip, zipPattern)) { throw new FormatException(); }
            else if (!Regex.IsMatch(city, cityPattern)) { throw new FormatException(); }
        }
    }
}