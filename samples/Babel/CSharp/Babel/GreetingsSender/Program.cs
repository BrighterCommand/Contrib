using System;
using GreetingsCore.Adapters.Factories;
using GreetingsCore.Ports.Events;
using GreetingsCore.Ports.Mappers;
using Paramore.Brighter;
using Paramore.Brighter.MessagingGateway.RMQ;
using Paramore.Brighter.MessagingGateway.RMQ.MessagingGatewayConfiguration;
using SimpleInjector;

namespace GreetingsSender
{
    class Program
    {
        static void Main(string[] args)
        {

            var container = new Container();
            var messageMapperFactory = new MessageMapperFactory(container);
            var messageMapperRegistry = new MessageMapperRegistry(messageMapperFactory)
            {
                { typeof(GreetingEvent), typeof(GreetingEventMessageMapper) }
            };
            
            var messageStore = new InMemoryMessageStore();
            var rmqConnnection = new RmqMessagingGatewayConnection 
            {
                AmpqUri  = new AmqpUriSpecification(new Uri("amqp://guest:guest@localhost:5672/%2f")),
                Exchange = new Exchange("paramore.brighter.exchange"),
            };
            var producer = new RmqMessageProducer(rmqConnnection);

            var builder = CommandProcessorBuilder.With()
                .Handlers(new HandlerConfiguration())
                .DefaultPolicy()
                .TaskQueues(new MessagingConfiguration(messageStore, producer, messageMapperRegistry))
                .RequestContextFactory(new InMemoryRequestContextFactory());

            var commandProcessor = builder.Build();

            commandProcessor.Post(new GreetingEvent("Ian"));
        }
    }
}