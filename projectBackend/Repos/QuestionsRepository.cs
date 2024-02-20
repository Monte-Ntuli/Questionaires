using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectBackend.Entities;
using projectBackend.Repos.Interfaces;


namespace projectBackend.Repos
{
    public class QuestionsRepository : Repository<QuestionsEntity>, IQuestionsRepository
    {

            private VillageDbContext _dbContext => (VillageDbContext)_context;
            public QuestionsRepository(VillageDbContext context) : base(context)
            {

            }

            public async override Task AddAsync(QuestionsEntity entity)
            {
                entity.Id = Guid.NewGuid();
                entity.QuestionID = GenerateQuestionID();
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }

        public async Task<QuestionsEntity> DeleteAsync(QuestionsEntity questionID)
        {
            var question = await _dbContext.Questions.FirstOrDefaultAsync(x => x.QuestionID == questionID.QuestionID);

            if (question == null)
            {
                return null;
            }
            _dbContext.Questions.Remove(question);
            await _dbContext.SaveChangesAsync();
            return question;
        }

        public async Task<List<QuestionsEntity>> GetAllQuestionsAsync(int QuestionareID)
        {
            //var questionID = await _dbContext.Questions.Where(x => x.QuestionaireID == QuestionareID).ToListAsync();
            var questions = await _dbContext.Questions.Where(y => y.QuestionaireID == QuestionareID).ToListAsync();

            if (questions != null )
            {
                return questions;
            }
            else
            {
                return null;
            }
        }

        private int GenerateQuestionID()
        {
            var highestId = _dbContext.Questions.OrderByDescending(x => x.QuestionID).FirstOrDefault();
            if (highestId != null)
            {
                return highestId.QuestionID + 1;
            }
            return 1;
        }
    }
}