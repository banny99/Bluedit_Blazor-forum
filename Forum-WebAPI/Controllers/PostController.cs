using Contracts.Services;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum_WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{

    private IPostService _postApiService;
    public PostController(IPostService postApiService)
    {
        _postApiService = postApiService;
    }

    [HttpGet]
    [Route("allposts")]
    public async Task<ActionResult<ICollection<Post>>> GetAllPosts()
    {
        try
        {
            var allPosts = await _postApiService.GetAsync();
            return Ok(allPosts);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("{postId:int}")]
    public async Task<ActionResult<Post>> GetPostById([FromRoute] int postId)
    {
        try
        {
            var post = await _postApiService.GetById(postId);
            return Ok(post);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    // [HttpGet]
    // [Route("{authorId:int}")]
    // public async Task<ActionResult<Post>> GetPostByAuthor([FromRoute] int authorId)
    // {
    //     try
    //     {
    //         var post = await _postApiService.GetByAuthor(authorId);
    //         return Ok(post);
    //     }
    //     catch (Exception e)
    //     {
    //         return StatusCode(500, e.Message);
    //     }
    // }
    
    
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Post>> DeletePostById([FromRoute] int id)
    {
        try
        {
            var deletedPost = await _postApiService.DeleteAsync(id);
            return Ok(deletedPost);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    [Route("newPost")]
    public async Task<ActionResult<Post>> CreateNewPost([FromBody] Post post)
    {
        try
        {
            var createdPost = await _postApiService.AddAsync(post);
            return Ok(createdPost);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}