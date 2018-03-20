using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class Message
    {
        public int Id { get; set; }

        public string MessageFrom { get; set; }

        [Required(ErrorMessage = "Select Receiver")]
        public string MessageTo { get; set; }

        public string Date { get; set; }

        [Required(ErrorMessage = "Title Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Message Required")]
        public string Details { get; set; }

        public bool Read { get; set; }
    }
}