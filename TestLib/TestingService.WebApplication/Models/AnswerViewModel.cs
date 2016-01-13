using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestingService.WebApplication.Models
{
    public class AnswerViewModel
    {
        public int QuestionId { get; set; }

        public int RadioButtonNumber { get; set; }

        public int AnswerCount { get; set; }

        public int QuestionType { get; set; }

        [Display(Name = "Mark for this answer")]
        public int? AnswerValue { get; set; }

        [Display(Name = "Question text")]
        public string Question { get; set; }

        [Display(Name = "Variants")]
        public string[] Answers { get; set; }

        [Display(Name = "Correct answers")]
        public string[] CorrectAnswers { get; set; }

        [Display(Name = "Correct answers")]
        public bool[] CorrectBoolAnswers { get; set; }
    }
}