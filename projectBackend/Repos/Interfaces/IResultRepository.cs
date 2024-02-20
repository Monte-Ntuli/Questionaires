using projectBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Repos.Interfaces
{
    public interface IResultRepository
    {
        Task AddAsync(ResultsEntity resultsEntity);
        Task<List<ResultsEntity>> GetAllAsync(string emailID);
    }
}
