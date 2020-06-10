namespace Whizbang.Core.EventSource
{
    public abstract class GenericEvent<T> : DomainEvent where T : class, new()
    {
        public T Data { get; set; }
    }
}