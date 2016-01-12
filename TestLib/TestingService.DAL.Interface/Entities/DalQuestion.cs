namespace TestingService.DAL.Interface.Entities
{
    public class DalQuestion:IEntity
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string QuestionStructure { get; set; }
        public int? QuestionNumberInTest { get; set; }
    }
}
