namespace Whizbang.Core.Commands
{
    /// <summary>
    ///     命令处理类型解析器接口
    /// </summary>
    public interface ICommandHanlderResolver
    {
        /// <summary>
        ///     解析命令处理类型
        /// </summary>
        /// <typeparam name="TCommand">命令类型</typeparam>
        /// <returns></returns>
        ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : class, ICommand;
    }
}