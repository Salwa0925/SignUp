using System.Data.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace UserApi;

/// <summary>
///  Auth Controller, handles user signup and login requests.
///  Provides endpoints for user registration and authentication.
/// </summary>
/// <param name="userService"></param>

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp(SignUpDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.CreateUser(dto);

            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(LogInDTO dto)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Login(dto);

            if (!result.Success)
                return Unauthorized(result.ErrorMessage);

            return Ok(result.Data);
        } 

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result.Data);
        }
    }
