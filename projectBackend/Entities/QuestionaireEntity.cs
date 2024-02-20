using projectBackend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Entities
{
    public class QuestionaireEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int QuestionaireID { get; set; }
        public string Questionaire { get; set; }
        public string Email { get; set; }
        public int QuestionID { get; set; }
        public string Link { get; set; }
    }
}
