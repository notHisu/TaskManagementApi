using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories;

public class UserController : ControllerBase
{
    private readonly IUserRepository<UserResponseDto> _userRepository;
    private readonly IAuthenticationService _authService;

    public UserController(IUserRepository<UserResponseDto> userRepository, IAuthenticationService authService )
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    [HttpPost("register")]
    public ActionResult<AuthResponseDto> Register([FromBody] UserCreateDto createDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdUser = _userRepository.Add(createDto);
            var token = _authService.GenerateToken(createdUser);
            
            return Ok(new AuthResponseDto 
            { 
                User = createdUser,
                Token = token
            });
        }
        catch (Exception ex) when (ex.Message.Contains("duplicate"))
        {
            return Conflict("Username already exists");
        }
    }

    [HttpPost("login")]
    public ActionResult<AuthResponseDto> Login([FromBody] UserLoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var authResult = _authService.Authenticate(loginDto.Username, loginDto.Password);
        
        if (authResult == null)
        {
            return Unauthorized("Invalid username or password");
        }

        return Ok(authResult);
    }

    [Authorize]
    [HttpGet(Name = "GetAllUsers")]
    public ActionResult<IEnumerable<UserResponseDto>> GetAllUsers()
    {
        var users = _userRepository.GetAll();
        return Ok(users);
    }


    [Authorize]
    [HttpGet("{id}", Name = "GetUserById")]
    public ActionResult<UserResponseDto> GetUserById(int id)
    {
        var user = _userRepository.GetById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

}