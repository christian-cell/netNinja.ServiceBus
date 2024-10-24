namespace netNinja.ServiceBus.Configurations
{
    public class ServiceBusConfiguration(string connectionString, string queueName)
    {
        public string ConnectionString { get; set; } = connectionString;
        public string QueueName { get; set; } = queueName;
    }
};

