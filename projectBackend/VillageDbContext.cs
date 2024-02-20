using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projectBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace projectBackend
{
    public class VillageDbContext : IdentityDbContext<AppUser,IdentityRole,string>
    {
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<UserEntity> UsersR { get; set; }
        public virtual DbSet<ResultsEntity> Results { get; set; }
        public virtual DbSet<QuestionaireEntity> Questionaires { get; set; }
        public virtual DbSet<QuestionsEntity> Questions { get; set; }
        public VillageDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}