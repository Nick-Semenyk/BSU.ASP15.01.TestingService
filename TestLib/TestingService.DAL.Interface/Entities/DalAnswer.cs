namespace TestingService.DAL.Interface.Entities
{
    public class DalAnswer:IEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int? AnswerValue { get; set; }
        public string AnswerStructure { get; set; }
    }
}
