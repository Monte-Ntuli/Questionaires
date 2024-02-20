using Microsoft.EntityFrameworkCore;
using projectBackend.Entities;
using projectBackend.Models.Requests;
using projectBackend.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Repos
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        private VillageDbContext _dbContext => (VillageDbContext)_context;

        public UserRepository(VillageDbContext context) : base(context)
        {

        }

        public async override Task AddAsync(UserEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.UserID = GenerateUserID();
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<UserEntity> UpdateAsync(UserEntity userID)
        {
            var user = await _dbContext.UsersR.FirstOrDefaultAsync(x => x.UserID == userID.UserID);
            if (user == null)
            {
                return null;
            }
            else
            {
                _dbContext.UsersR.Update(userID);
                 await _dbContext.SaveChangesAsync();
            }
         
            return user;
        }
        public async Task<UserEntity> DeleteAsync(UserEntity userID)
        {
            var user = await _dbContext.UsersR.FirstOrDefaultAsync(x => x.UserID == userID.UserID);

            if (user == null)
            {
                return null;
            }
            _dbContext.UsersR.Remove(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public override Task<UserEntity> GetAsync(Guid id)
        {
            return base.GetAsync(id);
        }

        public override Task<ICollection<UserEntity>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public async Task<bool> GetUser(UserEntity userEntity)
        {
            var user = await _dbContext.UsersR.FirstOrDefaultAsync(x => x.Email == userEntity.Email);
            if (user != null)
            {
                if (user.Password == userEntity.Password)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<List<UserEntity>> GetAllAsync(string boss)
        {
            var employees = await _dbContext.UsersR.Where(x => x.Boss == boss).ToListAsync();
            if (employees != null)
            {
                return employees;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserEntity> GetEmployee(int userID)
        {
            var employee = await _dbContext.UsersR.FirstOrDefaultAsync(x => x.UserID == userID);
            if (employee != null)
            { 
                return employee;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CheckUserForReset(UserEntity userEntity)
        {
            var user = await _dbContext.UsersR.FirstOrDefaultAsync(x => x.Email == userEntity.Email);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ResetPassword(UserEntity userEntity)
        {
            var user = await _dbContext.UsersR.FirstOrDefaultAsync(x => x.Email == userEntity.Email);
            if (user != null)
            {
                _dbContext.Entry(await _dbContext.UsersR.FirstOrDefaultAsync(x => x.Password == userEntity.Password)).CurrentValues.SetValues(userEntity);
                return (await _dbContext.SaveChangesAsync()) > 0;
            }

            return false;
        }

        private int GenerateUserID()
        {
            var highestId = _dbContext.UsersR.OrderByDescending(x => x.UserID).FirstOrDefault();
            if (highestId != null)
            {
                return highestId.UserID + 1;
            }
            return 1;
        }

    }
}
