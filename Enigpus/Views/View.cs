using Enigpus.Models;
using Enigpus.Services;
using Enigpus.Utils;

namespace Enigpus.Views;

public class View
{
    private readonly IInventoryService _inventoryService;

    public View(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public void Run()
    {
        string choice = "0";
        while (!choice.Equals("6"))
        {
            displayMenu();
            try
            {
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        SearchBook();
                        break;
                    case "3":
                        EditBook();
                        break;
                    case "4":
                        DeleteBook();
                        break;
                    case "5":
                        ViewAllBooks();
                        break;
                    case "6":
                        Console.WriteLine("Exiting....");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error ocurred: {e.Message}");
            }
        }
    }
    
    private void displayMenu(){
        Console.WriteLine();
        Console.WriteLine("Enigpus Inventory Menu:");
        Console.WriteLine("1. Add Book");
        Console.WriteLine("2. Search Book by Title");
        Console.WriteLine("3. Edit Book");
        Console.WriteLine("4. Delete Book by ID");
        Console.WriteLine("5. Get All Book");
        Console.WriteLine("6. Exit");
        Console.Write("Enter your choice: ");
    }

    private void AddBook()
    {
        string type;
        while (true)
        {
            type = Utility.InputString("Enter book type (1. Novel, 2. Magazine): ");
            if (type == "1" || type == "2")
            {
                break;
            }
            Console.WriteLine("Invalid book type");
        }

        var code = Utility.InputString("Enter book code (ex: A1): ");
        
        var title = Utility.InputString("Enter book title: ");
        var publisher = Utility.InputString("Enter publisher: ");
        var publishedYear = Utility.InputNumber("Enter year of publication: ");

        if (type == "1")
        {
            var author = Utility.InputString("Enter author: ");
            _inventoryService.AddBook(new Novel
            {
                Code = code,
                Title = title,
                Publisher = publisher,
                PublishedYear = publishedYear,
                Author = author
            });
        }
        else if (type == "2")
        {
            _inventoryService.AddBook(new Magazine
            {
                Code = code,
                Title = title,
                Publisher = publisher,
                PublishedYear = publishedYear,
            });
        }
    }

    private void SearchBook()
        {
            var title = Utility.InputString("Enter book title to search: ");
            var book = _inventoryService.SearchBookByTitle(title);

            if (book != null)
            {
                Console.WriteLine($"Book found: Code={book.Code}, Title={book.Title}, Publisher={book.Publisher}, Year={book.PublishedYear}");
                if (book is Novel novel)
                {
                    Console.WriteLine($"Author: {novel.Author}");
                }
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        private void EditBook()
        {
            var code = Utility.InputString("Enter book code to edit (ex: A1): ");
            var title = Utility.InputString("Enter new title: ");
            var publisher = Utility.InputString("Enter new publisher: ");
            var year = Utility.InputNumber("Enter new year of publication: ");
            string author = null;
            
            var book = _inventoryService.searchBookById(code);
            if (_inventoryService.searchBookById(code) is Novel)
            {
                author = Utility.InputString("Enter new author: ");
            }

            Book updatedBook = string.IsNullOrEmpty(author)
                ? new Magazine { Code = code, Title = title, Publisher = publisher, PublishedYear = year }
                : new Novel { Code = code, Title = title, Publisher = publisher, PublishedYear = year, Author = author };
            
            _inventoryService.UpdateBook(updatedBook);
        }

        private void DeleteBook()
        {
            var code = Utility.InputString("Enter book code to delete: (ex: A1): ");
            _inventoryService.DeleteBook(code);
        }

        private void ViewAllBooks()
        {
            var books = _inventoryService.getAllBooks();
            foreach (var book in books)
            {
                Console.WriteLine($"Code={book.Code}, Title={book.Title}, Publisher={book.Publisher}, Year={book.PublishedYear}");
                if (book is Novel novel)
                {
                    Console.WriteLine($"Author: {novel.Author}");
                }
            }
        }
}