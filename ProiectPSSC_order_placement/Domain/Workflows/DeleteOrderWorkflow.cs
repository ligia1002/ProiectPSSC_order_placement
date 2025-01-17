using System;
using System.Data.SqlClient;
using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;

using ProiectPSSC_order_placement.Domain.Models;
using ProiectPSSC_order_placement.Domain.Operations;
using ProiectPSSC_order_placement.Domain.Services;
using ProiectPSSC_order_placement.Domain.ValueObjects;
using System;
using static ProiectPSSC_order_placement.Domain.Models.Order;
using static ProiectPSSC_order_placement.Domain.Models.OrderPlacedEvent;


namespace ProiectPSSC_order_placement.Domain.Workflows
{
    public class DeleteOrderWorkflow
    {
        private const string ServiceBusConnectionString = "Endpoint=sb://coadamesaje.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=wQh/5fA5UlItW0JE8XCm8wky6BFLGz7SA+ASbPWJg1o=";
        private const string QueueName = "stergerecomenzi";
        private const string SqlConnectionString = "Server=tcp:comenziserverpssc.database.windows.net,1433;Initial Catalog=BazaPSSC;Persist Security Info=False;User ID=daniel.sighete;Password=@parola123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
       
        
        public void SendMessage(string value)
        {
            SendDeleteMessage(value);
        }

        static async Task SendDeleteMessage(string orderId)
        {
            await using var client = new ServiceBusClient(ServiceBusConnectionString);

            ServiceBusSender sender = client.CreateSender(QueueName);

            ServiceBusMessage message = new ServiceBusMessage(orderId);

            try
            {

                await sender.SendMessageAsync(message);
                Console.WriteLine($"Message sent: {orderId}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
            finally
            {
                await sender.DisposeAsync();
            }
        }
        public void Listener()
        {
            OrderListener listener = new OrderListener();
            listener.Listen(QueueName);
        }
    }
}
