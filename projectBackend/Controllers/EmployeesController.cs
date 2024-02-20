using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using projectBackend.DTO_s;
using projectBackend.Entities;
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
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeesController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public EmployeesController(IUnitOfWork unitOfWork, ILogger<EmployeesController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }

        #region Create Emplpoyee
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateUser([FromBody] CreateEmployeeDTO usersd)
        {
            try
            {
                if (usersd.Email == null || usersd.Email == "")
                {
                    return BadRequest();
                }
                await _unitOfWork.User.AddAsync(_mapper.Map<UserEntity>(usersd));
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

        #region Get All Employees
        [HttpGet("GetEmployees/{email}")]
        public async Task<IActionResult> GetAllEmployees(string email)
        {
                var employees = await _unitOfWork.User.GetAllAsync(email);
                return Accepted(employees);
        }
        #endregion

        #region Get specific employee
        [HttpGet("GetEmployee/{userID}")]
        public async Task<IActionResult> GetEmployee(int userID)
        {
            try
            {
                var employee = await _unitOfWork.User.GetEmployee(userID);
                return Accepted(employee);
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

        #region Delete specific employee
        [HttpPost("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee ([FromBody] DeleteEmployeeDTO userID)
        {
            try
            {
                var user = await _unitOfWork.User.DeleteAsync(_mapper.Map<UserEntity>(userID));
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

        #region UpdateEmployee
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateAsync([FromBody] CreateEmployeeDTO userID)
        {
            try
            {
                var user = await _unitOfWork.User.UpdateAsync(_mapper.Map<UserEntity>(userID));
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
