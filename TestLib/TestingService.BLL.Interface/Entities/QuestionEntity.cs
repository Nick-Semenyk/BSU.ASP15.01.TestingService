using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingService.BLL.Interface.Entities
{
    public class QuestionEntity
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string QuestionStructure { get; set; }
        public int? QuestionNumberInTest { get; set; }
    }
}
