using projectBackend.Entities;
using projectBackend.Models;
using projectBackend.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Repos.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity userEntity);
        Task<bool> GetUser(UserEntity userEntity);
        Task<bool> CheckUserForReset(UserEntity userEntity);
        Task<bool> ResetPassword(UserEntity userEntity);
        Task<List<UserEntity>> GetAllAsync(string userEntity);
        Task<UserEntity> GetEmployee(int userID);
        Task<UserEntity> DeleteAsync(UserEntity userID);
        Task<UserEntity> UpdateAsync(UserEntity userID);
    }
}
