public class JsonParsingException : Exception
{
    public string JsonBody { get; }
    public JsonParsingException() { }
    public JsonParsingException(string message) : base(message) { }
    public JsonParsingException(string message, Exception innerException) : base(message, innerException) { }
    public JsonParsingException(string message, string jsonBody) : base(message)
    {
        JsonBody = jsonBody;
    }
    public JsonParsingException(string message, string jsonBody, Exception innerException) : base(message, innerException)
    {
        JsonBody = jsonBody;
    }
}
