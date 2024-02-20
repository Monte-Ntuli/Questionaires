using projectBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Repos.Interfaces
{
    public interface IQuestionaireRepository
    {
        Task AddAsync(QuestionaireEntity questionaireEntity);
        Task<List<QuestionaireEntity>> GetAllAsync(string questionaireEntity);
        Task<QuestionaireEntity> DeleteAsync(QuestionaireEntity questionaireID);
        Task<QuestionaireEntity> PublishAsync(QuestionaireEntity questionaireID);
    }
}
