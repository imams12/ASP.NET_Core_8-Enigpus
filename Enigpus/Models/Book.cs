namespace Enigpus.Models;

public abstract class Book
{
    public string Code { get; set; }
    public string Title { get; set; }
    public string Publisher { get; set; }
    public int PublishedYear { get; set; }

    public abstract string getTitle();
}