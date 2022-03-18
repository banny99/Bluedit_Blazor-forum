using System.Text.Json;
using Entities.Models;

namespace JsonDataAccess.Context;

public class JsonContext
{
    private string forumPath = "forum.json";

    private Forum? forum;
    public Forum Forum
    {
        get
        {
            if (forum == null)
            {
                LoadData();
            }

            return forum!;
        }
        private set{}
    }

    public JsonContext()
    {
        if (File.Exists(forumPath))
        {
            LoadData();
        }
        else
        {
            CreateFile();
        }
    }

    private void CreateFile()
    {
        forum = new Forum();
        Task.FromResult(SaveChangesAsync());
    }

    private void LoadData()
    {
        string forumAsJson = File.ReadAllText(forumPath);
        forum = JsonSerializer.Deserialize<Forum>(forumAsJson)!;
    }

    public async Task SaveChangesAsync()
    {
        string forumAsJson = JsonSerializer.Serialize(forum, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = false
        });
        await File.WriteAllTextAsync(forumPath,forumAsJson);
        forum = null;
	}
}