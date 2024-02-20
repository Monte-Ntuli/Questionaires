using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Models
{
    public class ResultsModel
    {
        [Key]public int ResultID { get; set; }
        public QuestionsModel QuestionID { get; set; }
        public UserModel Email { get; set; }
        public QuestionaireModel QuestionaireID { get; set; }
        public double QuestionPoints { get; set; }
        public string Answer { get; set; }
       
    }
}
