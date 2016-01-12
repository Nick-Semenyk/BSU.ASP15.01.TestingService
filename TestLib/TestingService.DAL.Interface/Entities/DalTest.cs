using System;

namespace TestingService.DAL.Interface.Entities
{
    public class DalTest:IEntity
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
