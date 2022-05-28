// See https://aka.ms/new-console-template for more information

using Azure.Messaging.ServiceBus;

public class Program
{

    static string connectionString = "Endpoint=sb://learningazuremsgbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=kBtAbYrmlQoBAWzeADEA9mkbLo4G5FDco8Ng2pX8nHE=";

    static string topicName = "humberchat";

    static ServiceBusClient client;

    static ServiceBusSender sender;

    static async Task Main()
    {
        client = new ServiceBusClient(connectionString);
        sender = client.CreateSender(topicName);

        //using ServiceBusMessage message = await sender.CreateMessageAsync();

        while (true)
        {
            Console.WriteLine("Enter a message");
            string input = Console.ReadLine();
            if (input == "exit")
            {
                break;
            }

            ServiceBusMessage message = 
                new ServiceBusMessage($"John says:{input}");

            await sender.SendMessageAsync(message);

        }

        await sender.DisposeAsync();
        await client.DisposeAsync();

        Console.WriteLine("Messaging complete");
    }

}