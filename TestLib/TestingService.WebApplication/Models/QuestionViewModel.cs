using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestingService.WebApplication.Models
{
    public class QuestionViewModel
    {
        
        public int TestId { get; set; }

        [Display(Name = "Question number")]
        [Required]
        public int QuestionNumber { get; set; }

        [Display(Name = "Only one correct answer (radiobutton)")]
        public bool Radiobutton { get; set; }

        [Display(Name = "Checkbox")]
        public bool Checkbox { get; set; }

        [Display(Name = "Text answer")]
        public bool Textbox { get; set; }

        public int QuestionType { get; set; }

        [Display(Name = "Count of fields")]
        [Required]
        public int QuestionCount { get; set; }
    }
}