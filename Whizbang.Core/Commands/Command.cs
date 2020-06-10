using System;

namespace Whizbang.Core.Commands
{
    /// <summary>
    ///     命令基类
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <summary>
        ///     命令标识
        /// </summary>
        public virtual Guid Id { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (null == obj)
                return false;

            var command = obj as Command;

            return command != null && command.Id == Id;
        }
    }
}