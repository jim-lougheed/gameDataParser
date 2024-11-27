public class GameDataParserApp
{
    private readonly JSONReader _jsonReader;
    private readonly LogWriter _logWriter;
    public GameDataParserApp(JSONReader jsonReader, LogWriter logWriter)
    {
        _jsonReader = jsonReader;
        _logWriter = logWriter;
    }
    public void Run()
    {
        bool validFileName = false;
        string fileName = default;
        while (!validFileName)
        {
            Console.WriteLine("Enter the name of the file you want to read:");
            fileName = Console.ReadLine();
            validFileName = ValidateFilePath(fileName);
        }
        PrintGames(fileName);
        Console.WriteLine("Press any key to close.");
        Console.ReadKey();
    }

    public bool ValidateFilePath(string fileName)
    {
        if (fileName == null)
        {
            Console.WriteLine("File name cannot be null.");
            return false;
        }
        else if (fileName.Length == 0)
        {
            Console.WriteLine("File name cannot be empty.");
            return false;
        }
        else if (!File.Exists(fileName))
        {
            Console.WriteLine("File not found.");
            return false;
        }
        return true;
    }

    public void PrintGames(string fileName)
    {
        try
        {
            var gameData = _jsonReader.Read(fileName);
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
        }
        catch (JsonParsingException jsonParsingException)
        {
            _logWriter.LogException(jsonParsingException);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(jsonParsingException.Message + "JSON body: " + jsonParsingException.JsonBody);
            Console.WriteLine("Sorry! The application has experience an unexpected error and will have to be closed.");
            Console.ResetColor();
        }
    }
}
