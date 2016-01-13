using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestingService.WebApplication.Models
{
    public class QuestionTestViewModel
    {
        public string Question { get; set; }
        public string[] Variants { get; set; }
        public int QuestionType { get; set; }
    }
}