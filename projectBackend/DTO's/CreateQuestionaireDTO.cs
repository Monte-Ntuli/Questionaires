using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Models.Requests
{
    public class CreateQuestionaireDTO
    {
        public string Email { get; set; }
        public string Questionaire { get; set; }

    }
}
