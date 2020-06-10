namespace Whizbang.Core
{
    /// <summary>
    ///     基本处理器，所有处理器必须继承此接口
    /// </summary>
    /// <typeparam name="T">需要处理的信息类型</typeparam>
    public interface IHandler<in T>
    {
        void Handle(T message);
    }
}