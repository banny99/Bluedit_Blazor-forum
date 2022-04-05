using Contracts.Services;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum_WebAPI.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<User>>> GetAllUsers()
    {
        try
        {
            var allUsers = await _userService.GetAsync();
            return Ok(allUsers);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("{userId:int}")]
    public async Task<ActionResult<User>> GetUser([FromRoute]int userId)
    {
        try
        {
            var user = await _userService.GetById(userId);
            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("{username}")]
    public async Task<ActionResult<User>> GetUser([FromRoute]string username)
    {
        try
        {
            var user = await _userService.GetByUsername(username);
            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> AddUser([FromBody]User user)
    {
        try
        {
            var addedUser = await _userService.AddAsync(user);
            return Ok(addedUser);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}