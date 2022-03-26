using System.Text.Json;
using Entities.Models;

namespace FileData.JsonDataAccess;

public class JsonContext
{
    private const string ForumPath = "forum.json";

    private Forum? _forum;
    public Forum Forum
    {
        get
        {
            if (_forum == null)
            {
                LoadData();
            }

            return _forum!;
        }
        private set{}
    }

    public JsonContext()
    {
        if (File.Exists(ForumPath))
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
        _forum = new Forum();
        Task.FromResult(SaveChangesAsync());
    }

    private void LoadData()
    {
        string forumAsJson = File.ReadAllText(ForumPath);
        _forum = JsonSerializer.Deserialize<Forum>(forumAsJson)!;
    }

    public async Task SaveChangesAsync()
    {
        string forumAsJson = JsonSerializer.Serialize(_forum, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = false
        });
        await File.WriteAllTextAsync(ForumPath,forumAsJson);
        _forum = null;
	}
    
}