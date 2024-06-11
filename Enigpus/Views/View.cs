using System.Text.RegularExpressions;
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
                        Console.WriteLine(EditBook());
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

        string code;
        while (true)
        {
            code = Utility.InputString("Enter book code (ex: A1): ");
            if (Regex.IsMatch(code, @"^[A-Za-z]\d+$"))
            {
                break;   
            }
            Console.WriteLine("Invalid code");
        }
        
        var title = Utility.InputString("Enter book title: ");
        var publisher = Utility.InputString("Enter publisher: ");

        int publishedYear;
        while (true)
        {
            var input = Utility.InputString("Enter year of publication: ");
            if (Regex.IsMatch(input, @"^\d{4}$"))
            {
                publishedYear = Convert.ToInt32(input);
                break;
            }

            Console.WriteLine("Invalid year of publication");
        }

        if (type == "1")
        {
            var author = Utility.InputString("Enter author: ");
            string confirmation;
            while (true)
            {
                Console.WriteLine("Do you want to save this book? (yes/no)");
                confirmation = Console.ReadLine().ToLower();
                if (confirmation == "yes" || confirmation == "no")
                {
                    break;
                }

                Console.WriteLine("Invalid input");
            }
            if (confirmation.Equals("no"))
            {
                Console.WriteLine("Book has not been saved");
            }
            else
            {
                _inventoryService.AddBook(new Novel
                {
                    Code = code,
                    Title = title,
                    Publisher = publisher,
                    PublishedYear = publishedYear,
                    Author = author
                });

                Console.WriteLine("Book has been saved successfully");
            }
            
        }
        else if (type == "2")
        {
            string confirmation;
            while (true)
            {
                Console.WriteLine("Do you want to save this book? (yes/no)");
                confirmation = Console.ReadLine().ToLower();
                if (confirmation == "yes" || confirmation == "no")
                {
                    break;
                }

                Console.WriteLine("Invalid input");
            }
            if (confirmation.Equals("no"))
            {
                Console.WriteLine("Book has not been saved");
            }
            else
            {
                _inventoryService.AddBook(new Magazine
                {
                    Code = code,
                    Title = title,
                    Publisher = publisher,
                    PublishedYear = publishedYear,
                });
            
                Console.WriteLine("Book has been saved successfully");
            }
            
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

        private string EditBook()
        {
            if (_inventoryService.getAllBooks().Count() == 0)
            {
                 return "There is no book in inventory";
            }
            string code;
            while (true)
            {
                code = Utility.InputString("Enter book code (ex: A1): ");
                if (Regex.IsMatch(code, @"^[A-Za-z]\d+$"))
                {
                    break;   
                }
                Console.WriteLine("Invalid code");
            }
        
            var title = Utility.InputString("Enter book title: ");
            var publisher = Utility.InputString("Enter publisher: ");

            int year;
            while (true)
            {
                var input = Utility.InputString("Enter year of publication: ");
                if (Regex.IsMatch(input, @"^\d{4}$"))
                {
                    year = Convert.ToInt32(input);
                    break;
                }

                Console.WriteLine("Invalid year of publication");
            }
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

            return $"Book with {code} updated successfully";
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