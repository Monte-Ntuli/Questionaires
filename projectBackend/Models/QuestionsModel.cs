using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Models
{
        public class QuestionsModel
        {
            [Key]public int QuestionID { get; set; }
            public QuestionaireModel Questionaire { get; set; }
            public string Question { get; set; }
            public string QuestionType { get; set; }
            public double QuestionPoints { get; set; }
        }
}
