using projectBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Repos.Interfaces
{
    public interface IQuestionsRepository
    {
        Task AddAsync(QuestionsEntity questionsEntity);
        Task<List<QuestionsEntity>> GetAllQuestionsAsync(int QuestionareID);
        Task<QuestionsEntity> DeleteAsync(QuestionsEntity questionaireID);
    }
}
