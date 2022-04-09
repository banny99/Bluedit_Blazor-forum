using System.Text;
using System.Text.Json;
using Contracts.Services;
using Entities.Models;

namespace RESTClient.clients;

public class UserClient : IUserService
{

    const string Uri = "https://localhost:7058/users";
    public async Task<ICollection<User>> GetAsync()
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync(Uri);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<User> users = JsonSerializer.Deserialize<ICollection<User>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }

    public async Task<User> GetById(int id)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync(Uri +"/"+ id);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        User user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<User?> GetByUsername(string username)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync(Uri +"/"+ username);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        User user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<User?> AddAsync(User? user)
    {
        using HttpClient client = new ();
        string userAsJson = JsonSerializer.Serialize(user);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(Uri, content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }

        User returnedUser = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return returnedUser;
    }

    public async Task<User?> DeleteAsync(int userId)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.DeleteAsync(Uri +"/"+ userId);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        //+ delete all posts that belonged to that user
        await client.DeleteAsync("https://localhost:7058/posts/authors/" +userId);
        
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error:{response.StatusCode}, {responseContent}");
        }
        
        User deletedUser = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return deletedUser;
    }

    public Task<User?> UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }
}