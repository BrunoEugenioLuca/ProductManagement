using ProductManagement.Dal.Entity;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ProductManagement.Producer
{
    public class ProducerMessage : IProducerMessage
    {

        private string exchange = "Products";
        public async Task PushMessageAsync(Product product)
        {
            await Task.Run(() =>
            {
                try
                {
                    var factory = new ConnectionFactory()
                    {
                        Uri = new Uri("amqps://nwthmpgl:4wsjpaRHSPOk-Hy7CfE1rLT289PvT5bv@turkey.rmq.cloudamqp.com/nwthmpgl")
                    };
                    using (var connection = factory.CreateConnection())
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange, ExchangeType.Fanout, durable: true, autoDelete: false);

                        var text = JsonSerializer.Serialize(product);
                        
                        var body = Encoding.ASCII.GetBytes(text);
                        channel.BasicPublish(exchange: exchange,
                                                routingKey: "",
                                                basicProperties: null,
                                                body: body);

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");

                }
            });
        }
    }

    public interface IProducerMessage
    {
        Task PushMessageAsync(Product product);
    }
}