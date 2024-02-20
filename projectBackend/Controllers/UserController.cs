using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using projectBackend.DTO_s;
using projectBackend.Entities;
using projectBackend.Models;
using projectBackend.Models.Requests;
using projectBackend.Repos.Interfaces;
using projectBackend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtConfig _jwtConfig;
        private readonly IMailService mailService;

        public UserController(IUnitOfWork unitOfWork, ILogger<UserController> logger, IMapper mapper, IConfiguration config, 
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<JwtConfig> jwtConfig,
            IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtConfig = jwtConfig.Value;
            this.mailService = mailService;

        }

        #region Register new admin user
        [HttpPost("Register")] //Register a new user using Identity FrameWork... this is for admins
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO userRequestModel)
        {
            try
            {
                var user = new AppUser()
                {
                    FirstName = userRequestModel.FirstName,
                    Email = userRequestModel.Email,
                    UserName = userRequestModel.Email,
                    PhoneNumber = userRequestModel.PhoneNumber,
                    
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, userRequestModel.Password);
                if (result.Succeeded)
                {
                    return Accepted();
                }
                return BadRequest();

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

        #region Login the user
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserRequest)
        {
            try
            {
                bool admin = false;
                if (loginUserRequest.Email=="" || loginUserRequest.Password=="")
                {
                    return BadRequest();
                }
                var result = await _signInManager.PasswordSignInAsync(loginUserRequest.Email, loginUserRequest.Password, false, false); // Admins will be found in identity table
                if (result.Succeeded)
                {
                    var appUser = await _userManager.FindByEmailAsync(loginUserRequest.Email);
                    var user = new UserDTO(appUser.FirstName, appUser.LastName, appUser.Email, appUser.UserName, appUser.DateCreated);
                    user.Token = GenerateToken(appUser);
                    admin = true;
                    return Accepted(admin);
                }
                if (await _unitOfWork.User.GetUser(_mapper.Map<UserEntity>(loginUserRequest))) // Employees will be found in user table
                {
                    admin = false;
                    return Accepted();
                }
                else
                {
                    return BadRequest(admin);
                }
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

        #region Token generation
        private string GenerateToken(AppUser user) // Generate a token to user with identity framkework
        {
            var jwTokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] {
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtConfig.Audience,
                Issuer = _jwtConfig.Issuer,
            };
            var token = jwTokenhandler.CreateToken(tokenDescription);
            return jwTokenhandler.WriteToken(token);
        }
        #endregion

        #region GetLinkToResetPassword
        [HttpPost("GeneratePasswordResetLink")]
        public async Task<IActionResult> GenerateLink(MailDTO userd)
        {
            var appUser = await _userManager.FindByEmailAsync(userd.Email);

            try
            {
                if (await _unitOfWork.User.CheckUserForReset(_mapper.Map<UserEntity>(userd)))
                {
                    await mailService.SendResetEmailAsync(userd);
                    return Accepted();
                }
                if (appUser.Email == userd.Email)
                {
                    await mailService.SendResetEmailAsync(userd);
                    return Accepted();
                }
                else return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
            
        }
        #endregion

        #region Reset Password
        [HttpPatch("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO userDetails)
        {
            try
            {
                var appUser = await _userManager.FindByEmailAsync(userDetails.Email);
                if(userDetails.Email == null || userDetails.Email == "" || userDetails.Password == null || userDetails.Password == "")
                {
                    return BadRequest();
                }

                if (await _unitOfWork.User.CheckUserForReset(_mapper.Map<UserEntity>(userDetails)))
                {
                    await _unitOfWork.User.ResetPassword(_mapper.Map<UserEntity>(User));
                    return Accepted();
                }

                if (appUser.Email == userDetails.Email)
                {

                    var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

                    var result = await _userManager.ResetPasswordAsync(appUser, token, userDetails.Password);

                    return Accepted();

                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        #endregion
    }
}
