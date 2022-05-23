//using System.Threading.Tasks;
//using MassTransit;
//using Microsoft.Extensions.Logging;

//namespace Invictus.Application.Services
//{
//    public class GettingStartedConsumer :
//        IConsumer<GettingStarted>
//    {
//        readonly ILogger<GettingStartedConsumer> _logger;

//        public GettingStartedConsumer(ILogger<GettingStartedConsumer> logger)
//        {
//            _logger = logger;
//        }

//        public Task Consume(ConsumeContext<GettingStarted> context)
//        {
//            _logger.LogInformation("Received Text: {Text}", context.Message.Value);
//            return Task.CompletedTask;
//        }
//    }
//}