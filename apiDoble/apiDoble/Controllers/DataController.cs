using Microsoft.AspNetCore.Mvc;
using Azure.Messaging.ServiceBus;
using System;
using apiProductorParcial.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;

namespace apiProductorParcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpPost]
        public async Task<bool> EnviarAsync([FromBody] Data data)
        {

            var x = Int32.Parse(data.random);

            if (x % 2 == 0)//Enviar a qParMarco
            {

                //queue1 -> Shared access policies -> enviar -> Primary Connection String -> copiar 
                string connectionString = "Endpoint=sb://qparmarco.servicebus.windows.net/;SharedAccessKeyName=envia;SharedAccessKey=VsOQUZWsn0u4nh/8qfBi8C/riQSvtlMgPRziDMy+mmw=;EntityPath=queue2";
                string queueName = "queue2";
                //Instalar paquetes newton (JsonConvert)
                string mensaje = JsonConvert.SerializeObject(data);

                await using (ServiceBusClient client = new ServiceBusClient(connectionString))
                {
                    // Create a sender for the queue
                    ServiceBusSender sender = client.CreateSender(queueName);

                    // Create a message that we can send
                    ServiceBusMessage message = new ServiceBusMessage(mensaje);

                    // Send the message
                    await sender.SendMessageAsync(message);
                    Console.WriteLine($"Sent a single message to the queue: {queueName}");

                }


                return true;
            }
            else if (x % 2 == 1) //Enviar a qImpar
            {
                //queue1 -> Shared access policies -> enviar -> Primary Connection String -> copiar 
                string connectionString = "Endpoint=sb://qimparmarco.servicebus.windows.net/;SharedAccessKeyName=envia;SharedAccessKey=M+jx45FlRofKxyP9tbEyZxQW6F83Fb4pwrhmsD7/hsQ=;EntityPath=queue1";
                string queueName = "queue1";
                //Instalar paquetes newton (JsonConvert)
                string mensaje = JsonConvert.SerializeObject(data);

                await using (ServiceBusClient client = new ServiceBusClient(connectionString))
                {
                    // Create a sender for the queue
                    ServiceBusSender sender = client.CreateSender(queueName);

                    // Create a message that we can send
                    ServiceBusMessage message = new ServiceBusMessage(mensaje);

                    // Send the message
                    await sender.SendMessageAsync(message);
                    Console.WriteLine($"Sent a single message to the queue: {queueName}");

                }


                return true;
            }
            else
                return false;
        }
    }
}
