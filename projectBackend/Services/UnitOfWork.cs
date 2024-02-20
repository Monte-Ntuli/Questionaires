using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectBackend.Repos;
using projectBackend.Repos.Interfaces;
using projectBackend.Services.Interfaces;


namespace projectBackend.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly VillageDbContext _context;

        public UnitOfWork(VillageDbContext context)
        {
            _context = context;
        }

        //public IidentityUserRepository _idUser;

        //public IidentityUserRepository IDUser
        //{
        //    get
        //    {
        //        if (_idUser == null)
        //            _idUser = new IdentityUserRepository(_context);

        //        return _idUser;
        //    }
        //}

        public IUserRepository _user;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                    _user = new UserRepository(_context);

                return _user;
            }
        }

        public IQuestionaireRepository _questionaire;
        public IQuestionaireRepository Questionaire
        {
            get
            {
                if (_questionaire == null)
                    _questionaire = new QuestionaireRepository(_context);

                return _questionaire;
            }
        }

        public IResultRepository _results;

        public IResultRepository Result
        {
            get
            {
                if (_results == null)
                    _results = new ResultRepository(_context); //this.context = (IContect)new CCISEntities();

                return _results;
            }
        }

        public IQuestionsRepository _questions;
        public IQuestionsRepository Questions
        {
            get
            {
                if (_questions == null)
                    _questions = new QuestionsRepository(_context);

                return _questions;
            }
        }

    }
}
