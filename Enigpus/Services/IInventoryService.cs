using Enigpus.Models;

namespace Enigpus.Services;

public interface IInventoryService
{
    void AddBook(Book book);
    Book SearchBookByTitle(string title);
    Book searchBookById(string code);
    Book UpdateBook(Book book);
    List<Book> getAllBooks();
    void DeleteBook(string id);
}