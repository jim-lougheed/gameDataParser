using System.Text.Json;

try
{
    var gameDataParserApp = new GameDataParserApp(new StringsJSONRepository());
    gameDataParserApp.Run();
}
catch (Exception exception)
{
    Console.WriteLine(exception.ToString());
}

public class GameDataParserApp
{
    private readonly StringsRepository _stringsRepository;
    public GameDataParserApp(StringsRepository stringsRepository)
    {
        _stringsRepository = stringsRepository;
    }
    public void Run()
    {
        bool validFileName = false;
        while (!validFileName)
        {
            Console.WriteLine("Enter the name of the file you want to read:");
            var fileName = Console.ReadLine();
            if (fileName == null)
            {
                Console.WriteLine("File name cannot be null.");
            }
            else if (fileName.Length == 0)
            {
                Console.WriteLine("File name cannot be empty.");
            } 
            else if (!File.Exists(fileName)) 
            {
                Console.WriteLine("File not found.");
            } else
            {
                validFileName = true;
                try
                {
                    var gameData = _stringsRepository.Read(fileName);
                    if (gameData.Count() > 0)
                    {
                        Console.WriteLine("Loaded games are:");
                        foreach (var game in gameData)
                        {
                            Console.WriteLine(game);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No games are present in the input file.");
                    }
                } catch (JsonParsingException jsonParsingException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(jsonParsingException.Message + jsonParsingException.JsonBody);
                    Console.WriteLine("Sorry! The application has experience an unexpected error and will have to be closed.");
                    Console.ResetColor();
                }
                
            }
        }
        Console.WriteLine("Press any key to close.");
        Console.ReadKey();
    }
}

public class JsonParsingException : Exception
{
    public string JsonBody { get;  }
    public JsonParsingException() { }
    public JsonParsingException(string message) : base(message) { }
    public JsonParsingException(string message, Exception innerException) : base(message, innerException) { }
    public JsonParsingException(string message, string jsonBody) : base(message) {
        JsonBody = jsonBody;
    }
    public JsonParsingException(string message, string jsonBody, Exception innerException) : base(message, innerException)
    {
        JsonBody = jsonBody;
    }
}

public abstract class StringsRepository
{
    public abstract List<Game> TextToStrings(string fileContents);
    public List<Game> Read(string filePath)
    {
        var fileContents = File.ReadAllText(filePath);
        try
        {
            return TextToStrings(fileContents);
        }
        catch (JsonException jsonException)
        {
            throw new JsonParsingException($"JSON in the {filePath} was not in a valid format. JSON body: ", fileContents);
        }
    }
}

public class StringsJSONRepository : StringsRepository
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    public override List<Game> TextToStrings(string fileContents)
    {
        return JsonSerializer.Deserialize<List<Game>>(fileContents, _options);
    }
}

public class Game
{
    public override string ToString()
    {
        return $"{Title}, released in {ReleaseYear}, rating: {Rating}";
    }
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public float Rating { get; set; }
}