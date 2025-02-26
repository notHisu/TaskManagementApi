using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationService _authService;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, IAuthenticationService authService, SignInManager<User> signInManager)
        {
            _authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] UserCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = createDto.Username,
                Email = createDto.Email
            };

            var createdUser = await _userManager.CreateAsync(user, createDto.Password);

            if (createdUser == null)
            {
                return BadRequest("User could not be created");
            }

            if (createdUser.Succeeded)
            {
                return Ok(new AuthResponseDto
                {
                    User = new UserResponseDto
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Email = user.Email
                    },
                    Token = _authService.GenerateToken(user)
                });
            }
            else
            {
                return StatusCode(500, createdUser.Errors);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

                if (user == null)
                {
                    return Unauthorized("Invalid username");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (!result.Succeeded)
                {
                    return Unauthorized("Invalid password");
                }

                return Ok(new AuthResponseDto
                {
                    User = new UserResponseDto
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Email = user.Email
                    },
                    Token = _authService.GenerateToken(user)
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet(Name = "GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers()
        {
            try
            {
                var users = await _userManager.Users.Select(x => new UserResponseDto
                {
                    Id = x.Id,
                    Username = x.UserName,
                    Email = x.Email
                }).ToListAsync();
                return Ok(users);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<UserResponseDto> GetUserById(string id)
        {
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}