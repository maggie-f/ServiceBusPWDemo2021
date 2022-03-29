using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Sender
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://programmersweek.servicebus.windows.net/;SharedAccessKeyName=sender;SharedAccessKey=0JOMqRKwparCu9wvwz1RRnNG7Z0lyPSvokMqMYOo9Gk=;";
        const string QueueName = "gobeyond";
        static IQueueClient queueClient;
        
        public static async Task Main(string[] args)
        {
            var messagesToSend = GetMessagesToSend();
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");

            // Send messages.
            await SendMessagesAsync(messagesToSend);

            Console.ReadKey();

            await queueClient.CloseAsync();
        }

        static async Task SendMessagesAsync(string[] messagesToSend)
        {
            try
            {
                for (var i = 0; i < messagesToSend.Length; i++)
                {
                    // Create a new message to send to the queue.
                    string messageBody = messagesToSend[i];
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    // Write the body of the message to the console.
                    Console.WriteLine($"Sending message: {messageBody}");

                    // Send the message to the queue.
                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }

        private static string[] GetMessagesToSend()
        {
            string[] messages = new string[]
            {
                "Programmers' week 2021",
                "Go beyond",
                "Cognizant Softvision",
                "Azure",
                "Service Bus",
            };

            return messages;
        }
    }
}
