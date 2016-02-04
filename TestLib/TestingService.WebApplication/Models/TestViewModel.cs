using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestingService.WebApplication.Models
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int QuestionCount { get; set; }
        public DateTime CreationDate { get; set; }

        [Display(Name = "Name")]
        [Required]
        [RegularExpression(@".{4,50}", ErrorMessage = "Name should be from 4 to 50 characters long")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required]
        [RegularExpression(@".{10,500}", ErrorMessage = "Description should be from 10 to 500 characters long")]
        public string Description { get; set; }

        [Display(Name = "Available to anyone")]
        public bool GlobalAvailability { get; set; }

        [Display(Name = "Anonymous")]
        public bool Anonymous { get; set; }

        [Display(Name = "Interview")]
        public bool Interview { get; set; }

        [Display(Name = "Time in seconds for test")]
        public int AllowedTime { get; set; }
    }
}