using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using projectBackend.DTO_s;
using projectBackend.Entities;
using projectBackend.Models;
using projectBackend.Models.Requests;
using projectBackend.Services.Interfaces;


namespace projectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ResultsController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ResultsController(IUnitOfWork unitOfWork, ILogger<ResultsController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }

        #region Get All Answered Questionaires
        [HttpGet("GetAnsweredQuestionaires/{emailID}")]
        public async Task<IActionResult> GetAnsweredQuestionaires(string emailID)
        {
            try
            {
                var questionaires = await _unitOfWork.Result.GetAllAsync(emailID);
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
    }
}
