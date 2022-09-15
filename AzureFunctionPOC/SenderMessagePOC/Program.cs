using Azure.Messaging.ServiceBus;
using System;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace SenderMessagePOC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await ServiceBusSenderHelper.SendMessage();
        }
    }

    public class ServiceBusSenderHelper
    {
        public static async Task SendMessage()
        {
            try
            {
                while (true)
                {
                    string connectionString = "";
                    string topicName = "";

                    await using var client = new ServiceBusClient(connectionString);

                    ServiceBusSender sender = client.CreateSender(topicName);

                    var removeShoppingCartMessage = new RemoveShoppingCartMessage
                    {
                        CustomerId = Guid.NewGuid(),
                    };

                    var json = JsonSerializer.Serialize(removeShoppingCartMessage);

                    var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(json));
                    var sessionId = Guid.NewGuid().ToString();
                    message.SessionId = sessionId;
                    //message.MessageId = Guid.NewGuid().ToString();

                    await Task.Delay(500);
                    await sender.SendMessageAsync(message);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
