using Enigpus.Models;

namespace Enigpus.Services.Impls;

public class InventoryServiceImpl : IInventoryService
{
    private List<Book> _books = new List<Book>();

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public Book SearchBookByTitle(string title)
    {
        var existingBook = _books.Find(book => book.Title.ToLower().Equals(title.ToLower()));
        return existingBook;
    }

    public Book searchBookById(string code)
    {
        var existingBook = _books.Find(book => book.Code.Equals(code));
        return existingBook;
    }

    public Book UpdateBook(Book book)
    {
        var existingBook = _books.Find(b => b.Code.Equals(book.Code));
        if (existingBook != null)
        {
            existingBook.Title = !string.IsNullOrEmpty(book.Title) ? book.Title : existingBook.Title;
            existingBook.Publisher = !string.IsNullOrEmpty(book.Publisher) ? book.Publisher : existingBook.Publisher;
            existingBook.PublishedYear = book.PublishedYear != 0 ? book.PublishedYear : existingBook.PublishedYear;
            
            if (existingBook is Novel && book is Novel)
            {
                var existingNovel = existingBook as Novel;
                var newNovel = book as Novel;

                existingNovel.Author = !string.IsNullOrEmpty(newNovel.Author) ? newNovel.Author : existingNovel.Author;
            }
        }

        return existingBook;  
    }

    public List<Book> getAllBooks()
    {
        return _books;
    }

    public void DeleteBook(string id)
    {
        var bookToRemove = _books.Find(book => book.Code.Equals(id));
        if (bookToRemove != null)
        {
            _books.Remove(bookToRemove);
        }
        else
        {
            throw new ArgumentException($"Book with id {id} not found.");
        }
    }
}