using System;
using fncImpar.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace fncImpar
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async System.Threading.Tasks.Task RunAsync(

            [ServiceBusTrigger(
                    "queue1",
                    Connection = "MyConn"
            )] string myQueueItem,

            [CosmosDB(
                    databaseName:"dbImpar",
                    collectionName:"Eventos",
                    ConnectionStringSetting = "strCosmos"
             )] IAsyncCollector<object> datos,

            ILogger log)
        {
            try
            {
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
                var data = JsonConvert.DeserializeObject<Data>(myQueueItem);
                await datos.AddAsync(data); 
            }
            catch (Exception ex)
            {
                log.LogError($"No es posible insertar datos: {ex.Message}");
            }
        }
    }
}
