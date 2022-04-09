using Contracts.Services;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum_WebAPI.Controllers;

[ApiController]
[Route("posts")]
public class PostController : ControllerBase
{

    private IPostService _postService;
    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Post>>> GetAllPosts()
    {
        try
        {
            var allPosts = await _postService.GetAsync();
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
            var post = await _postService.GetById(postId);
            return Ok(post);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("authors/{authorId:int}")]
    public async Task<ActionResult<ICollection<Post>>> GetPostsByAuthor([FromRoute] int authorId)
    {
        try
        {
            var post = await _postService.GetPostsByAuthorId(authorId);
            return Ok(post);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpDelete]
    [Route("{postId:int}")]
    public async Task<ActionResult<Post>> DeletePostById([FromRoute] int postId)
    {
        try
        {
            var deletedPost = await _postService.DeleteAsync(postId);
            return Ok(deletedPost);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpPost]
    public async Task<ActionResult<Post>> CreateNewPost([FromBody] Post post)
    {
        try
        {
            var createdPost = await _postService.AddAsync(post);
            return Ok(createdPost);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPut]
    public async Task<ActionResult<Post>> EditPost([FromBody] Post post)
    {
        try
        {
            var editedPost = await _postService.UpdateAsync(post);
            return Ok(editedPost);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}