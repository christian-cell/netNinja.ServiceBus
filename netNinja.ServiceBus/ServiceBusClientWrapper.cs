using Azure.Messaging.ServiceBus;
using netNinja.ServiceBus.Configurations;

namespace netNinja.ServiceBus
{
    public class ServiceBusClientWrapper
    {
        public ServiceBusClient Client { get; set; }
        public ServiceBusSender Sender { get; set; }

        public ServiceBusClientWrapper(ServiceBusConfiguration config, ServiceBusClient? client = null, ServiceBusSender? sender = null)
        {
            Client = client ?? new ServiceBusClient(config.ConnectionString);
            Sender = sender ?? Client.CreateSender(config.QueueName);
            InitializeClient(config);
        }

        private void InitializeClient(ServiceBusConfiguration config)
        {
            Client = new ServiceBusClient(config.ConnectionString);
            Sender = Client.CreateSender(config.QueueName);
        }

        public async Task SendMessageAsync(string message)
        {
            try
            {
                var busMessage = new ServiceBusMessage(message);
                await Sender.SendMessageAsync(busMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
};

