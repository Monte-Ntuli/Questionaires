using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Models
{
    public class QuestionaireModel
    {
        [Key]public int QuestionaireID { get; set; }
        public string Questionaire { get; set; }
        public UserModel Email { get; set; }
    }
}
