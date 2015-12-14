using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SchoolChoicePlayground.Models
{
    public class Alert
    {
        [Key]
        public int AlertId { get; set; }
        public string message { get; set; }
        public DateTime dateToSend { get; set; }
    }
}