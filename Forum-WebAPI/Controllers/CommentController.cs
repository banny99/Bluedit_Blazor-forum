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
    
    [HttpDelete]
    [Route("{commentId:int}")]
    public async Task<ActionResult<Comment>> DeleteCommentById([FromRoute]int commentId)
    {
        try
        {
            var deletedPost = await _commentService.DeleteCommentById(commentId);
            return Ok(deletedPost);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("authors/{authorId:int}")]
    public async Task<ActionResult<Comment>> DeleteCommentsByAuthorId([FromRoute]int authorId)
    {
        try
        {
            var deletedPosts = await _commentService.DeleteCommentsByAuthorId(authorId);
            return Ok(deletedPosts);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("posts/{postId:int}")]
    public async Task<ActionResult<Comment>> DeleteCommentsByPostId([FromRoute]int postId)
    {
        try
        {
            var deletedPosts = await _commentService.DeleteCommentsByPostId(postId);
            return Ok(deletedPosts);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}