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
        Console.WriteLine("Enter the name of the file you want to read:");
        List<string> videoGameData;
        bool validFileName = false;
        while (!validFileName)
        {
            var fileName = Console.ReadLine();
            if (fileName == null)
            {
                Console.WriteLine("File name is null");
            }
            else if (fileName.Length == 0)
            {
                Console.WriteLine("File name is empty.");
            } 
            else if (!File.Exists(fileName)) 
            {
                Console.WriteLine("File name doesn't exist");
            } else
            {
                validFileName = true;
                var gameData = _stringsRepository.Read(fileName);
                Console.WriteLine("SUCCESS");
                if (gameData.Count() > 0)
                {
                    foreach (var game in gameData)
                    {
                        Console.WriteLine(game);
                    }
                } else
                {
                    Console.WriteLine("No games are present in the input file.");
                }
            }
        }
        Console.ReadKey();
    }
}

public abstract class StringsRepository
{
    public abstract List<Game> TextToStrings(string fileContents);
    public List<Game> Read(string filePath)
    {
        if (File.Exists(filePath))
        {
            var fileContents = File.ReadAllText(filePath);
            try
            {
                return TextToStrings(fileContents);
            } 
            catch (JsonException ex)
            {
                Console.WriteLine("Json exception", ex.Message, ex.StackTrace);
                return new List<Game>();
            }
            //foreach (var game in gameData)
            //{
            //    Console.WriteLine(game.Title);
            //    Console.WriteLine(game.ReleaseYear);
            //    Console.WriteLine(game.Rating);
            //}
            //return TextToStrings(fileContents);
        }
        return new List<Game>();
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