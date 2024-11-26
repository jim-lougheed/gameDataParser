try
{
    var gameDataParserApp = new GameDataParserApp();
    gameDataParserApp.Run();
}
catch (Exception exception)
{
    Console.WriteLine(exception.ToString());
}

public class GameDataParserApp
{
    private readonly StringsRepository _stringsRepository;
    public void Run()
    {
        Console.WriteLine("Enter the name of the file you want to read:");
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
                //var videoGameData = _stringsRepository.Read(filePath);
            }
        }
        Console.WriteLine("SUCCESS");
        Console.ReadKey();
    }
}

public class StringsRepository
{

}