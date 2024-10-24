using Azure.Messaging.ServiceBus;
using Moq;
using netNinja.ServiceBus.Configurations;
using Xunit;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace netNinja.ServiceBus.Tests
{
    public class ServiceBusClientWrapperTests
    {

        private readonly IConfiguration _configuration;

        public ServiceBusClientWrapperTests()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddXmlFile("config.runsettings.xml", optional: false);

            _configuration = configBuilder.Build();
        }
        
        [Fact]
        public async Task SendMessageAsync_ShoudSendMessage()
        {
            
            var config = new ServiceBusConfiguration(
                _configuration.GetSection("TestRunParameters:Parameter:ServiceBusConnectionString:value").Value,
                _configuration.GetSection("TestRunParameters:Parameter:ServiceBusQueueName:value").Value
                );
            
            var mockSender = new Mock<ServiceBusSender>();

            mockSender.Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), default))
                .Returns(Task.CompletedTask);

            var mockClient = new Mock<ServiceBusClient>();

            mockClient.Setup(c => c.CreateSender(It.IsAny<string>()))
                .Returns(mockSender.Object);

            var serviceBusClientWrapper = new ServiceBusClientWrapper(config)
            {
                Client = mockClient.Object,
                Sender = mockSender.Object
            };

            var message = "Hello Service Bus!";
            
            //ACT
            await serviceBusClientWrapper.SendMessageAsync(message);
            
            //ASSERT
            mockSender.Verify(s => s.SendMessageAsync(It.Is<ServiceBusMessage>(m => m.Body.ToString() == message) , default)
            ,Times.Once);
        }
    
    }
};

