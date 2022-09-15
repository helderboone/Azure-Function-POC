using AzureFunctionPOC.Messages;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionPOC;

public class ShoppingCartEventListener
{
    private const string FunctionName = nameof(ShoppingCartEventListener);
    private readonly ILogger<ShoppingCartEventListener> _logger;

    public ShoppingCartEventListener(ILogger<ShoppingCartEventListener> log)
    {
        _logger = log;
    }

    /*
    * Topic with Session Enable and DQL ON
    * Although it is set auto complete false on host.json it seems that
    * it does not apply when deployed on Azure Function App
    */
    [FunctionName(nameof(ShoppingCartEventListener))]
    public async Task Run([ServiceBusTrigger("%ServiceBusShoppingCart:TopicName%",
        "%ServiceBusShoppingCart:Subscription%",
        Connection = "ServiceBusShoppingCart:ServiceBusConnString",
        IsSessionsEnabled = true, 
        AutoComplete = false)]
        Message message,
        MessageReceiver messageReceiver,
        ILogger logger)
    {
        try
        {
            string strMessage = Encoding.UTF8.GetString(message.Body);

            logger.LogInformation($"{FunctionName} topic trigger function processed message: {strMessage}");

            var removeShoppingCartMessage = JsonConvert.DeserializeObject<RemoveShoppingCartMessage>(strMessage);

            await messageReceiver.CompleteAsync(message.SystemProperties.LockToken);

            logger.LogInformation($"{FunctionName} has processed: {strMessage} successfully");
        }
        catch (Exception ex)
        {
            logger.LogInformation("Sending message to Dead Letter Queue");
            logger.LogError($"{ex.Message}");
            await messageReceiver.DeadLetterAsync(message.SystemProperties.LockToken);
        }
    }
}
