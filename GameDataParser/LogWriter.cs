public class LogWriter
{
    private static readonly string Separator = Environment.NewLine;
    private string _filePath { get; set; }
    public LogWriter(string filePath)
    {
        _filePath = filePath;
    }

    public void LogException(Exception exception)
    {
        List<string> log = Read();
        DateTime now = DateTime.Now;
        string formattedNow = now.ToString();
        string newLog = $"[{formattedNow}], ExceptionMessage: {exception.Message}, Stack trace: {exception.StackTrace}.";
        log.Add(newLog);
        Write(log);
    }
    public List<string> TextToStrings(string fileContents)
    {
        return fileContents.Split(Separator).ToList();
    }
    public List<string> Read()
    {
        if (File.Exists(_filePath))
        {
            var fileContents = File.ReadAllText(_filePath);
            return TextToStrings(fileContents);
        }
        return new List<string>();
    }
    public string StringsToText(List<string> strings)
    {
        return string.Join(Separator, strings);
    }
    public void Write(List<string> strings)
    {
        File.WriteAllText(_filePath, StringsToText(strings));
    }
}
