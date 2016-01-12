using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingService.BLL.Interface.Entities
{
    public class AnswerEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int? AnswerValue { get; set; }
        public string AnswerStructure { get; set; }
    }
}
