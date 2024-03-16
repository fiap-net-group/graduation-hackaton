using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Event;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Logger;
using MassTransit;

namespace Graduation.Hackaton.VideoProcessing.Infrastructure.Event
{
    public class MassTransitSenderGateway : IEventSenderGateway
    {
        private readonly IBus _bus;
        private readonly ILoggerGateway _logger;

        public MassTransitSenderGateway(IBus bus, ILoggerGateway logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task<Guid> SendAsync<TEvent, TEventBody>(TEvent genericEvent, CancellationToken cancellationToken) where TEvent : BaseEvent<TEventBody>
        {
            _logger.Log("Sending event", LoggerManagerSeverity.DEBUG,
                (LoggingConstants.Event, genericEvent));

            if (genericEvent.RequestId == Guid.Empty)
                genericEvent.RequestId = Guid.NewGuid();

            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{genericEvent.OperationName}"));

            await endpoint.Send(genericEvent);

            _logger.Log("Event sent", LoggerManagerSeverity.DEBUG,
                (LoggingConstants.Event, genericEvent),
                (LoggingConstants.RequestId, genericEvent.RequestId));

            return genericEvent.RequestId;
        }
    }
}
