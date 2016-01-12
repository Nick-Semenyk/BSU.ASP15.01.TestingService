using System;

namespace TestingService.DAL.Interface.Entities
{
    public class DalUsersAnswers : IEntity
    {
        public int Id { get; set; }
        public int UserTestId { get; set; }
        public int QuestionId { get; set; }
        public DateTime BeginningTime { get; set; }
        public DateTime? EndingTime { get; set; }
        public string AnswerStructure { get; set; }
    }
}
