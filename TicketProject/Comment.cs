namespace TicketProject;

public class Comment
{
    public string Author { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; private set; } = DateTime.Now;
        
    public Comment(string author, string text)
    {
        Author = author;
        Text = text;
    }
}