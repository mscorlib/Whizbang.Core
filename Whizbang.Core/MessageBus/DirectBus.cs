using System.Collections.Generic;
using Whizbang.Core.Extensions;

namespace Whizbang.Core.MessageBus
{
    public abstract class DirectBus : IBus
    {
        //contructor
        protected DirectBus()
        {
            //_queue = new ConcurrentQueue<IMessage>();
            _distributor = App.Container.Resolve<IMessageDistributor>();
        }

        /// <summary>
        ///     发布消息
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="message">消息实例</param>
        public void Publish<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            //_queue.Enqueue(message);

            //Committed = false;

            ////--todo:IBus inherit IUnitOfWork and manual commit
            //Commit();

            _distributor.Distribute(message);
        }

        /// <summary>
        ///     批量发布消息
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="messages">消息集合</param>
        public void Publish<TMessage>(IEnumerable<TMessage> messages) where TMessage : class, IMessage
        {
            //messages.Each(message => _queue.Enqueue(message));

            //Committed = false;

            ////--todo:IBus inherit IUnitOfWork and manual commit
            //Commit();

            messages.Each(message => _distributor.Distribute(message));
        }

        ///// <summary>
        /////     消息是否提交
        ///// </summary>
        //public bool Committed { get; private set; }

        ///// <summary>
        /////     提交
        ///// </summary>
        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public void Commit()
        //{
        //    _backupMessageArray = new IMessage[_queue.Count];

        //    _queue.CopyTo(_backupMessageArray, 0);

        //    _queue.TryOut(messate=>_distributor.Distribute(messate));

        //    Committed = true;
        //}

        ///// <summary>
        /////     回滚
        ///// </summary>
        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public void Rollback()
        //{
        //    if (_backupMessageArray != null && _backupMessageArray.Length > 0)
        //    {
        //        Clear();

        //        _backupMessageArray.Each(message=>_queue.Enqueue(message));
        //    }

        //    Committed = false;
        //}

        //public void Clear()
        //{
        //    Interlocked.Exchange(ref _queue, new ConcurrentQueue<IMessage>());
        //}

        #region private fields

        private readonly IMessageDistributor _distributor;

        //private ConcurrentQueue<IMessage> _queue;

        //private IMessage[] _backupMessageArray;

        #endregion private fields
    }
}