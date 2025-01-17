using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProiectPSSC_order_placement.Domain.Models;
using ProiectPSSC_order_placement.Domain.Operations;
using ProiectPSSC_order_placement.Domain.Services;
using ProiectPSSC_order_placement.Domain.Validation;
using ProiectPSSC_order_placement.Domain.ValueObjects;


public class OrderListener
    {
        private const string ServiceBusConnectionString = "Endpoint=sb://coadamesaje.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=wQh/5fA5UlItW0JE8XCm8wky6BFLGz7SA+ASbPWJg1o=";
        private const string SqlConnectionString = "Server=tcp:comenziserverpssc.database.windows.net,1433;Initial Catalog=BazaPSSC;Persist Security Info=False;User ID=daniel.sighete;Password=@parola123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public async Task Listen(string QueueName)
        {

            await using var client = new ServiceBusClient(ServiceBusConnectionString);


            ServiceBusReceiver receiver = client.CreateReceiver(QueueName);

            Console.WriteLine("Listening for messages...");

            try
            {
                while (true)
                {

                    ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync();

                    if (message != null)
                    {
                        string orderId = message.Body.ToString();
                        Console.WriteLine($"Received message: Delete order with ID {orderId}");


                        DeleteOrderFromDatabase(orderId);


                        await receiver.CompleteMessageAsync(message);
                    }
                    else
                    {
                        Console.WriteLine("No messages in the queue.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {

                await receiver.DisposeAsync();
            }
        }

        static void DeleteOrderFromDatabase(string orderId)
        {
            string query = "DELETE FROM Orders WHERE Id = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(SqlConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", orderId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Order with ID {orderId} deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Order with ID {orderId} not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting order from database: {ex.Message}");
            }
        }

    }

