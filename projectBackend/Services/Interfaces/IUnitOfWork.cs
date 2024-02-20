using projectBackend.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Services.Interfaces
{
        public interface IUnitOfWork
        {
            IQuestionsRepository Questions { get; }
            IQuestionaireRepository Questionaire { get; }
            IResultRepository Result { get; }
            IUserRepository User { get; }
            //IidentityUserRepository IDUser { get; }
    }

}
