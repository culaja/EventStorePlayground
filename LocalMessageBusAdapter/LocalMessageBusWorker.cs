using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Common;
using Common.Messaging;
using static Common.Nothing;

namespace LocalMessageBusAdapter
{
    internal sealed class LocalMessageBusWorker : ILocalMessageBus, IDisposable
    {
        private readonly ILocalMessageBus _localMessageBus;
        private readonly Thread _workerThread;
        private readonly BlockingCollection<IMessage> _dispatchedMessages = new BlockingCollection<IMessage>();
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        
        public LocalMessageBusWorker(ILocalMessageBus localMessageBus)
        {
            _localMessageBus = localMessageBus;
            _workerThread = new Thread(Worker);
            _workerThread.Start();
        }

        private void Worker()
        {
            while (CancellationIsNotRequested) 
                WaitForMessageAndHandleIt();
        }

        private bool CancellationIsNotRequested => !_tokenSource.IsCancellationRequested;

        private void WaitForMessageAndHandleIt()
        {
            try
            {
                _localMessageBus.Dispatch(_dispatchedMessages.Take(_tokenSource.Token));
            }
            catch (OperationCanceledException)
            {
            }
        }

        public Nothing DispatchAll(IReadOnlyList<IMessage> messages)
        {
            messages.Map(Dispatch);
            return NotAtAll;
        }

        public Nothing Dispatch(IMessage message)
        {
            _dispatchedMessages.Add(message);
            return NotAtAll;
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _workerThread.Join();
        }
    }
}