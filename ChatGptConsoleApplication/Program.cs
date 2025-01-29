// See https://aka.ms/new-console-template for more information

using ChatGptConsoleApplication.Services;

namespace ChatGptConsoleApplication;

internal static class Program
{
    public static async Task Main()
    {
        Console.WriteLine("Hello, please enter the API key");
        var apiKey = Console.ReadLine();

        if (apiKey == null)
        {
            Console.WriteLine("API Key not entered. The application will be terminated!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("API Key is entered. Thank you");
        var openAiService = new OpenAiService(apiKey);
        if (openAiService == null) throw new ArgumentNullException(nameof(openAiService));

        Console.WriteLine("Welcome to the ChatGPT chatbot! Type 'exit' to quit.");
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("You: ");
            Console.ResetColor();
            var input = Console.ReadLine() ?? string.Empty;
            
            if (input.ToLower() == "exit")
                break;

            var response = await openAiService.GetResponseAsync(input);

            // Display the chatbot's response
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Chatbot: ");
            Console.ResetColor();
            Console.WriteLine(response);
        }
    }
}