using System.Text.Json;
using Contracts.Services;
using Entities.Models;

namespace Application.Logic;

public class PostClientImpl : IPostService
{
    
    
    public async Task<ICollection<Post>> GetAsync()
    {
        // HttpClient client = new HttpClient();
        // HttpContent content = new StringContent()
        //     
        // HttpResponseMessage responseMessage = await client.GetAsync("lo");
        // string postAsJson = responseMessage.ToString();
        // var retrievedPosts = JsonSerializer.Deserialize<ICollection<Post>>(postAsJson);
        // return retrievedPosts;
        
        throw new NotImplementedException();
    }

    public Task<Post> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetByAuthor(int authorId)
    {
        throw new NotImplementedException();
    }

    public Task<Post> AddAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public Task<Post> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Post> UpdateAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public void AddComment(Post post, Comment comment)
    {
        throw new NotImplementedException();
    }
}