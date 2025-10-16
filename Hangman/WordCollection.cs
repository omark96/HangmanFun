using System.Text.Json;

namespace Hangman;

internal class WordCollection
{
    public List<string> Easy { get; set; }
    public List<string> Medium { get; set; }
    public List<string> Hard { get; set; }

    public WordCollection()
    {
        Easy = new();
        Medium = new();
        Hard = new();
    }

    public void LoadWords(string path)
    {
        string json = File.ReadAllText(path);
        WordCollection words = JsonSerializer.Deserialize<WordCollection>(json) ?? GetDefault();
        Easy = words.Easy;
        Medium = words.Medium;
        Hard = words.Hard;
    }

    public static WordCollection GetDefault()
    {
        return new WordCollection
        {
            Easy = new List<string> { "cat", "dog", "sun" },
            Medium = new List<string> { "planet", "rocket", "forest" },
            Hard = new List<string> { "photosynthesis", "quarantine", "xylophone" }
        };
    }
}