using Microsoft.AspNetCore.Http;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectBackend.Entities;
using projectBackend.Models;
using projectBackend.Services.Interfaces;
using projectBackend.Models.Requests;
using projectBackend.DTO_s;

namespace projectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<QuestionsController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public QuestionsController(IUnitOfWork unitOfWork, ILogger<QuestionsController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }

        #region Create Question
        [HttpPost("CreateQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionDTO QuestionDetails)
        {
            try
            {
                await _unitOfWork.Questions.AddAsync(_mapper.Map<QuestionsEntity>(QuestionDetails));
                return Accepted("Question Created");
            }
            catch (Exception ex)
            {
                var error = new ErrorModel
                {
                    title = ex.Message
                };

                return BadRequest(error);
            }
        }
        #endregion

        #region Get All Questions
        [HttpGet("GetQuestions/{QuestionaireID}")]
        public async Task<IActionResult> GetAllQuestionsAsync (int QuestionaireID)
        {
            try
            {
                var questionaires = await _unitOfWork.Questions.GetAllQuestionsAsync(QuestionaireID);
                return Accepted(questionaires);
            }
            catch (Exception ex)
            {
                var error = new ErrorModel
                {
                    title = ex.Message
                };

                return BadRequest(error);
            }

        }
        #endregion

        #region Delete Specific queston
        [HttpPost("DeleteQuestion")]
        public async Task<IActionResult> DeleteQuestion([FromBody] DeleteQuestionDTO questionID)
        {
            try
            {
                var question = await _unitOfWork.Questions.DeleteAsync(_mapper.Map<QuestionsEntity>(questionID));
                return Accepted();
            }
            catch (Exception ex)
            {
                var error = new ErrorModel
                {
                    title = ex.Message
                };

                return BadRequest(error);
            }
        }
        #endregion

    }
}
