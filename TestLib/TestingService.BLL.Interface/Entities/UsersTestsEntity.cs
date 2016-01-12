using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingService.BLL.Interface.Entities
{
    public class UsersTestsEntity
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
