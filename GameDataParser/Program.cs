try
{
    var gameDataParserApp = new GameDataParserApp(new JSONReader(), new LogWriter("log.txt"));
    gameDataParserApp.Run();
}
catch (Exception exception)
{
    var _logWriter = new LogWriter("log.txt");
    _logWriter.LogException(exception);
}
