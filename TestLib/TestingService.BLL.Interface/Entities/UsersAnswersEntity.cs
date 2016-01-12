using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingService.BLL.Interface.Entities
{
    public class UsersAnswersEntity
    {
        public int UserTestId { get; set; }
        public int QuestionId { get; set; }
        public DateTime BeginningTime { get; set; }
        public DateTime? EndingTime { get; set; }
        public string AnswerStructure { get; set; }
    }
}
