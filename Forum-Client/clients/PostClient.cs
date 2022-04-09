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
        HttpResponseMessage response = await client.GetAsync(Uri);
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

    public async Task<ICollection<Post>> GetPostsByAuthorId(int authorId)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync(Uri +"/authors/"+ authorId);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
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

    public async Task<Post> DeleteAsync(int id)
    {
        HttpClient client = new HttpClient();
        
        HttpResponseMessage response = await client.DeleteAsync(Uri +"/"+ id);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error:{response.StatusCode}, {responseContent}");
        }
        
        Post deletedPost = JsonSerializer.Deserialize<Post>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return deletedPost;
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        HttpClient client = new HttpClient();
        string postAsJson = JsonSerializer.Serialize(post);
        StringContent content = new StringContent(postAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PutAsync(Uri, content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error:{response.StatusCode}, {responseContent}");
        }

        Post editedPost = JsonSerializer.Deserialize<Post>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return editedPost;
    }
    
}