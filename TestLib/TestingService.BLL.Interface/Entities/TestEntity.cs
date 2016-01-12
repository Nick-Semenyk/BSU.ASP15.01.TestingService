using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingService.BLL.Interface.Entities
{
    public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public bool GlobalAvailability { get; set; }
        public bool Anonymous { get; set; }
        public bool Interview { get; set; }
        public int AllowedTime { get; set; }
        public DateTime CreationDate { get; set; }
        public int QuestionCount { get; set; }
        public int? RequiredQuestionCount { get; set; }
    }
}
