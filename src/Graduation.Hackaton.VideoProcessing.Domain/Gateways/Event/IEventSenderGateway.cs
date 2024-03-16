namespace Graduation.Hackaton.VideoProcessing.Domain.Gateways.Event
{
    public interface IEventSenderGateway
    {
        Task<Guid> SendAsync<TEvent, TEventBody>(TEvent genericEvent, CancellationToken cancellationToken) where TEvent : BaseEvent<TEventBody>;
    }
}
