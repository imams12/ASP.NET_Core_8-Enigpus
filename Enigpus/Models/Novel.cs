namespace Enigpus.Models;

public class Novel : Book
{
    public string Author { get; set; }
    
    public override string getTitle()
    {
        return Title;
    }
}