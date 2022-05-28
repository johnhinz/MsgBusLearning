
using Azure.Messaging.ServiceBus;

class Program
{    
    static string  connectionString = "Endpoint=sb://learningazuremsgbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=";

    static string topicName = "humberchat";

    static string subscriptionName = "HumberChatSubPup";

    static ServiceBusClient client;

    static ServiceBusProcessor processor;

    static async Task MessageHandler(ProcessMessageEventArgs arg)
    {
        Console.WriteLine($"Received: {arg.Message.Body.ToString()}");
        await arg.CompleteMessageAsync(arg.Message);
    }

    static Task ErrorHandler(ProcessErrorEventArgs arg)
    {
        Console.WriteLine(arg.Exception.ToString());
        return Task.CompletedTask;
    }

    static async Task Main()
    {
        client = new ServiceBusClient(connectionString);
        processor =
            client.CreateProcessor(
                topicName, subscriptionName);

        processor.ProcessMessageAsync += MessageHandler;
        processor.ProcessErrorAsync += ErrorHandler;

        await processor.StartProcessingAsync(); 

        Console.WriteLine("Listneing for messages");
        Console.ReadLine(); 

        await processor.StopProcessingAsync();

        await processor.DisposeAsync();
        await client.DisposeAsync();


    }
}
