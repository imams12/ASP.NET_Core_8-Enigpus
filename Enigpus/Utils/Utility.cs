namespace Enigpus.Utils;

public class Utility
{
    public static string InputString(string prompt)
    {
        string input;
        while (true)
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && !string.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                Console.WriteLine("Input cannot be empty. Please try again");
            }
        }
    }

    public static int InputNumber(string prompt)
    {
        int input;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out input))
            {
                return input;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number");
            }
        }
    }
}