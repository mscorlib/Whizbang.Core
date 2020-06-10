namespace Whizbang.Core.Commands
{
    public abstract class GenericCommand<T> : Command where T : class, new()
    {
        public T Data { get; set; }
    }
}