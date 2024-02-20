using projectBackend.Entities;
using projectBackend.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace projectBackend.Repos
{
    public class QuestionaireRepository : Repository<QuestionaireEntity>, IQuestionaireRepository
    {

        private VillageDbContext _dbContext => (VillageDbContext)_context;
        public QuestionaireRepository(VillageDbContext context) : base(context)
        {

        }

        public async override Task AddAsync(QuestionaireEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.QuestionaireID = GenerateQuestionaireID();
            entity.Link = "0";
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<QuestionaireEntity> PublishAsync(QuestionaireEntity questionaireID)
        {
            var questionaire = await _dbContext.Questionaires.FirstOrDefaultAsync(x => x.QuestionaireID == questionaireID.QuestionaireID);
            
            if (questionaire == null)
            {
                return null;
            }
            if (questionaireID.Link == "1")
            {
                questionaireID.Link = "0";
                return questionaire;
            }
            else if (questionaireID.Link == "0")
            {
                questionaire.Link = "1";
                return questionaire;
            }
            else if (questionaireID.Link != "1" || questionaireID.Link != "0")
            {
                questionaire.Link = "1";
                await _dbContext.SaveChangesAsync();
                return questionaire;
            }
            
            return questionaire;
        }

        public async Task<QuestionaireEntity> DeleteAsync(QuestionaireEntity questionaireID)
        {
            var questionaire = await _dbContext.Questionaires.FirstOrDefaultAsync(x => x.QuestionaireID == questionaireID.QuestionaireID);
            
            if (questionaire == null)
            {
                return null;
            }

            var questions = _dbContext.Questions.Where(x => x.QuestionaireID == questionaireID.QuestionaireID).ToList();

            if (questions == null)
            {
                return null;
            }
           
            _dbContext.Questionaires.Remove(questionaire);
            _dbContext.Questions.RemoveRange(questions);
            await _dbContext.SaveChangesAsync();
            return questionaire;
        }
        //Task<List<questionaireEntity>>  List<ProjectClass> result = db.Data.Where(model => model.collection == “aaa”).ToList(); 
        public async Task<List<QuestionaireEntity>> GetAllAsync(string questionaireEntity)
        {
            
            var questionair = await _dbContext.Questionaires.Where(x => x.Email == questionaireEntity).ToListAsync();
            if (questionair != null)
            {
                return questionair;
            }
            else
            {
                return null;
            }

        }

        private int GenerateQuestionaireID()
        {
            var highestId = _dbContext.Questionaires.OrderByDescending(x => x.QuestionaireID).FirstOrDefault();
            if (highestId != null)
            {
                return highestId.QuestionaireID + 1;
            }
            return 1;
        }

    }
}
