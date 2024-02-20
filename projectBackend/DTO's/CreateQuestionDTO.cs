using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Models.Requests
{
    public class CreateQuestionDTO
    {
        public int QuestionaireID { get; set; }
        public string Question { get; set; }
        public string QuestionType { get; set; }
        public double QuestionPoints { get; set; }

    }
}
