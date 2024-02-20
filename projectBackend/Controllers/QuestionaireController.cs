using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using projectBackend.DTO_s;
using projectBackend.Entities;
using projectBackend.Models;
using projectBackend.Models.Requests;
using projectBackend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionaireController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<QuestionaireController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public QuestionaireController(IUnitOfWork unitOfWork, ILogger<QuestionaireController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }

        #region Create Questionaire
        [HttpPost("CreateQuestionaire")]
        public async Task<IActionResult> CreateQuestionaire(CreateQuestionaireDTO questionaire)
        {
            try
            {
                await _unitOfWork.Questionaire.AddAsync(_mapper.Map<QuestionaireEntity>(questionaire));
                return Accepted("Questionaire Created");
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

        #region Get All Questionairs
        [HttpGet("GetQuestionaires/{email}")]
        public async Task<IActionResult> GetAllQuestionaires(string email)
        {
            try
            {
                var questionaires = await _unitOfWork.Questionaire.GetAllAsync(email);
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

        #region Delete Questionaire and all related Questions
        [HttpPost("DeleteQuestionaire")]
        public async Task<IActionResult> DeleteQuestionaire([FromBody] DeleteQuestionaireDTO questionaireID)
        {
            try
            {
                var questionaire = await _unitOfWork.Questionaire.DeleteAsync(_mapper.Map<QuestionaireEntity>(questionaireID));
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

        #region PublishQuestionaire
        [HttpPost("PublishQuestionaire")]
        public async Task<IActionResult> PublishQuestionaire(string questionaire)
        {
            try
            {
                
                await _unitOfWork.Questionaire.PublishAsync(_mapper.Map<QuestionaireEntity>(questionaire));
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
