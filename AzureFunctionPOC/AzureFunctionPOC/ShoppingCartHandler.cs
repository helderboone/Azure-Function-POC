using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctionPOC
{
    public class ShoppingCartHandler
    {
        private readonly ILogger<ShoppingCartHandler> _logger;

        public ShoppingCartHandler(ILogger<ShoppingCartHandler> log)
        {
            _logger = log;
        }


        /*
        * Topic with Session Enable and DQL ON
        * 
        */
        [FunctionName(nameof(ShoppingCartHandler))]
        public async Task Run([ServiceBusTrigger("mytopic", "mysubscription", Connection = "Teste")]string mySbMsg)
        {
            
        }
    }
}
