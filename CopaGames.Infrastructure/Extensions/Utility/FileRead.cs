using Newtonsoft.Json;

namespace CopaGames.Infrastructure.Extensions.Utility;

public static class FileRead
{
    public static async Task<string> ReadFileAsString(string filePath)
    {
        using var reader = new StreamReader(filePath);
        var fileString = await reader.ReadToEndAsync() ??
            throw new ArgumentException($"Could not read file at {filePath}");

        return fileString;
    }

    public static async Task<(string, T)> ReadJsonFromFile<T>(string filePath)
    {
        var jsonString = await ReadFileAsString(filePath);
        var deserializedObject = JsonConvert.DeserializeObject<T>(jsonString) ??
                                 throw new ArgumentException( $"Could not serialize object from JSON file {filePath}");
        return (jsonString, deserializedObject);
    }
}
