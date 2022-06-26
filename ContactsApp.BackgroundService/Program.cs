// See https://aka.ms/new-console-template for more information

using ContactsApp.BackgroundService;
using ContactsApp.BackgroundService.Clients;

var consumer = KafkaConsumer.RaiseConsumer();

CancellationTokenSource cancellationTokenSource =new();

try
{
    while (true)
    {
        var response = consumer.Consume(cancellationTokenSource.Token);
        if (response.Message != null)
        {
            var value = response.Message.Value;
            
            Console.WriteLine(value);
            var items = await ContactClient.GetContactInformationsAsync();

            foreach (var item in items)
            {
                Console.WriteLine(item);
                
            }
        }

    }
}
catch(Exception ex)
{
    Console.WriteLine(ex);
}