using Microsoft.EntityFrameworkCore;
using projectBackend.Entities;
using projectBackend.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Repos
{
    public class ResultRepository : Repository<ResultsEntity>, IResultRepository
    {

        private VillageDbContext _dbContext => (VillageDbContext)_context;
        public ResultRepository(VillageDbContext context) : base(context)
        {

        }

        public async override Task AddAsync(ResultsEntity entity)
        {
            entity.Id = Guid.NewGuid();
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ResultsEntity>> GetAllAsync(string emailID)
        {

            var questionair = await _dbContext.Results.Where(x => x.Email == emailID).ToListAsync();
            if (questionair != null)
            {
                return questionair;
            }
            else
            {
                return null;
            }

        }

    }
}
