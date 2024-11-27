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