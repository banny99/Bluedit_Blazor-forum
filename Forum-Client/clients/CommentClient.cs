using System.Text;
using System.Text.Json;
using Contracts.Services;
using Entities.Models;

namespace RESTClient.clients;

public class CommentClient : ICommentService
{
    const string Uri = "https://localhost:7058/comments";


    public async Task<ICollection<Comment>> GetAllComments()
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync(Uri);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<Comment>? comments = JsonSerializer.Deserialize<ICollection<Comment>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return comments;
    }

    public async Task<Comment> GetCommentById(int commentId)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync(Uri +"/"+ commentId);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        Comment comment = JsonSerializer.Deserialize<Comment>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return comment;
    }

    public async Task<ICollection<Comment>> GetCommentsByPostId(int postId)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync(Uri +"/posts/"+ postId);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<Comment>? comments = JsonSerializer.Deserialize<ICollection<Comment>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return comments;
    }

    public async Task<ICollection<Comment>> GetCommentsByAuthorId(int authorId)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync(Uri +"/authors/"+authorId);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<Comment>? comments = JsonSerializer.Deserialize<ICollection<Comment>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return comments;
    }


    public async Task<Comment> AddComment(Comment comment)
    {
        HttpClient client = new HttpClient();
        string commentAsJson = JsonSerializer.Serialize(comment);
        StringContent content = new StringContent(commentAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync(Uri, content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error:{response.StatusCode}, {responseContent}");
        }

        Comment addedComment = JsonSerializer.Deserialize<Comment>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return addedComment;
    }

    public async Task<Comment> DeleteCommentById(int commentId)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.DeleteAsync(Uri +"/"+ commentId);
        string responseContent = await response.Content.ReadAsStringAsync();


        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error:{response.StatusCode}, {responseContent}");
        }
        
        Comment deletedComment = JsonSerializer.Deserialize<Comment>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return deletedComment;
    }

    public async Task<ICollection<Comment>> DeleteCommentsByAuthorId(int authorId)
    {
        HttpClient client = new HttpClient();
        
        HttpResponseMessage response = await client.DeleteAsync(Uri +"/authors/"+ authorId);
        string responseContent = await response.Content.ReadAsStringAsync();


        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error:{response.StatusCode}, {responseContent}");
        }
        
        ICollection<Comment> deletedComments = JsonSerializer.Deserialize<ICollection<Comment>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return deletedComments;
    }

    public async Task<ICollection<Comment>> DeleteCommentsByPostId(int postId)
    {
        HttpClient client = new HttpClient();
        
        HttpResponseMessage response = await client.DeleteAsync(Uri +"/posts/"+ postId);
        string responseContent = await response.Content.ReadAsStringAsync();


        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error:{response.StatusCode}, {responseContent}");
        }
        
        ICollection<Comment> deletedComments = JsonSerializer.Deserialize<ICollection<Comment>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return deletedComments;
    }
}