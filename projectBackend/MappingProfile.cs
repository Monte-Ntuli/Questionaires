using AutoMapper;
using projectBackend.DTO_s;
using projectBackend.Entities;
using projectBackend.Models;
using projectBackend.Models.Requests;
using projectBackend.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend
{
    public class MappingProfile : Profile
    {
            public MappingProfile()
            {
            // Add as many of these lines as you need to map your objects
            #region Questions Map
            CreateMap<QuestionsEntity, QuestionsModel>().ReverseMap();
            CreateMap<QuestionsEntity, CreateQuestionDTO>().ReverseMap();
            CreateMap<QuestionsEntity, DeleteQuestionDTO>().ReverseMap();
            #endregion

            #region Questionaire Map
            CreateMap<QuestionaireEntity, QuestionaireModel>().ReverseMap();
            CreateMap<QuestionaireEntity, CreateQuestionaireDTO>().ReverseMap();
            CreateMap<QuestionaireEntity, GetQuestionairesDTO>().ReverseMap();
            CreateMap<QuestionaireEntity, DeleteQuestionaireDTO>().ReverseMap();
            #endregion

            #region User Map
            CreateMap<UserEntity, UserModel>().ReverseMap();
            CreateMap<UserEntity, CreateEmployeeDTO>().ReverseMap();
            CreateMap<UserEntity, LoginUserDTO>().ReverseMap();
            CreateMap<UserEntity, MailDTO>().ReverseMap();
            CreateMap<UserEntity, ResetPasswordDTO>().ReverseMap();
            CreateMap<UserEntity, DeleteEmployeeDTO>().ReverseMap();
            CreateMap<UserEntity, CreateQuestionaireDTO>().ReverseMap();
            #endregion

            #region Results Map
            CreateMap<ResultsEntity, GetAnsweredQuestionairesDTO>().ReverseMap();
            #endregion
        }
    }
}
