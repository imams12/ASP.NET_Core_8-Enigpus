using Enigpus.Services.Impls;
using Enigpus.Views;

public class Program
{
    public static void Main(string[] args)
    {
        Run();
    }

    public static void Run()
    {
        var inventoryService = new InventoryServiceImpl();

        var view = new View(inventoryService);
        view.Run();
    }
}