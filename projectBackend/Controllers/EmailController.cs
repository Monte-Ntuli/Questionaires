using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projectBackend.Models.Requests;
using projectBackend.Services.Interfaces;
using projectBackend.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace projectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailService mailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmailController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public EmailController(IMailService mailService, IUnitOfWork unitOfWork, ILogger<EmailController> logger, IMapper mapper, IConfiguration config)
        {
            this.mailService = mailService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }

        //[HttpPost("SendMail")]
        //public async Task<IActionResult> SendMail([FromForm] MailRequestsModel mailRequestModel)
        //{
        //    try
        //    {
        //        await mailService.SendEmailAsync(mailRequestModel);
        //        return Accepted();
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = new ErrorModel
        //        {
        //            title = ex.Message
        //        };

        //        return BadRequest(error);
        //    }
        //}
        
    }
}
