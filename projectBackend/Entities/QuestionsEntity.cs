using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Entities
{
    public class QuestionsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int QuestionID { get; set; }
        public int QuestionaireID { get; set; }
        public string Question { get; set; }
        public string QuestionType { get; set; }
        public string ResultID { get; set; }

    }
}
