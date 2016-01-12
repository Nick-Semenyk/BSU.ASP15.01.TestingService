using System;

namespace TestingService.DAL.Interface.Entities
{
    public class DalUsersTest : IEntity
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        public DateTime BeginningTime { get; set; }
        public DateTime? EndingTime { get; set; }
        public int? RightAnswersCount { get; set; }
        public int? MarkForTest { get; set; }
    }
}
