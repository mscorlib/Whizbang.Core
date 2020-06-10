namespace Whizbang.Core.Commands
{
    /// <summary>
    ///     命令处理器接口
    /// </summary>
    /// <typeparam name="TCommand">要处理的命令类型</typeparam>
    public interface ICommandHandler<in TCommand> : IHandler<TCommand> where TCommand : ICommand
    {
    }
}