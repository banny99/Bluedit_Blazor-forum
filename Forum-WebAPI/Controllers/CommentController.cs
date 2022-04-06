using Contracts.Services;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum_WebAPI.Controllers;

[ApiController]
[Route("comments")]
public class CommentController : ControllerBase
{
    private ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<Comment>>> GetAllComments()
    {
        try
        {
            var allComments = await _commentService.GetAllComments();
            return Ok(allComments);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("{commentId:int}")]
    public async Task<ActionResult<Comment>> GetCommentById([FromRoute] int commentId)
    {
        try
        {
            var allComments = await _commentService.GetCommentById(commentId);
            return Ok(allComments);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("posts/{postId:int}")]
    public async Task<ActionResult<ICollection<Comment>>> GetCommentsByPostId([FromRoute]int postId)
    {
        try
        {
            var allComments = await _commentService.GetCommentsByPostId(postId);
            return Ok(allComments);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("authors/{authorId:int}")]
    public async Task<ActionResult<ICollection<Comment>>> GetCommentsByAuthorId([FromRoute]int authorId)
    {
        try
        {
            var allComments = await _commentService.GetCommentsByAuthorId(authorId);
            return Ok(allComments);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Comment>> AddComment([FromBody]Comment comment)
    {
        try
        {
            var createdPost = await _commentService.AddComment(comment);
            return Ok(createdPost);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}