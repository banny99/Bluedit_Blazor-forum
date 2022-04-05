using System.Text;
using System.Text.Json;
using Contracts.Services;
using Entities.Models;

namespace RESTClient.clients;

public class PostClient : IPostService
{
    
    const string Uri = "https://localhost:7058/posts";
    
    public async Task<ICollection<Post>?> GetAsync()
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync("https://localhost:7058/posts");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<Post>? posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<Post> GetById(int id)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync(Uri + $"/{id}");
        // HttpResponseMessage response = await client.GetAsync("https://localhost:7058/posts/0");
        
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        Post post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }

    public Task<Post> GetByAuthor(int authorId)
    {
        throw new NotImplementedException();
    }

    public async Task<Post> AddAsync(Post post)
    {
        HttpClient client = new HttpClient();
        string postAsJson = JsonSerializer.Serialize(post);
        StringContent content = new StringContent(postAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync(Uri, content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error:{response.StatusCode}, {responseContent}");
        }

        Post addedPost = JsonSerializer.Deserialize<Post>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return addedPost;
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