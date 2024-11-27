using System.Text.Json;

public class JSONReader
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    public List<Game> TextToStrings(string fileContents)
    {
        return JsonSerializer.Deserialize<List<Game>>(fileContents, _options);
    }
    public List<Game> Read(string filePath)
    {
        var fileContents = File.ReadAllText(filePath);
        try
        {
            return TextToStrings(fileContents);
        }
        catch (JsonException jsonException)
        {
            throw new JsonParsingException($"JSON in the {filePath} was not in a valid format.", fileContents);
        }
    }
}
