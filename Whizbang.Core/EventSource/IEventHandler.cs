namespace Whizbang.Core.EventSource
{
    /// <summary>
    ///     事件处理器
    /// </summary>
    /// <typeparam name="TEvent">事件类型</typeparam>
    public interface IEventHandler<in TEvent> : IHandler<TEvent> where TEvent : IEvent
    {
    }
}