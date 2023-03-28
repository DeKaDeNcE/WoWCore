// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using System.Threading;
using Framework.Threading;

namespace Framework.Database
{
    public interface ISqlOperation
    {
        bool Execute<T>(MySqlBase<T> mySqlBase);
    }

    class DatabaseWorker<T>
    {
        Thread _workerThread;
        volatile bool _cancelationToken;
        ProducerConsumerQueue<ISqlOperation> _queue;
        MySqlBase<T> _mySqlBase;

        public DatabaseWorker(ProducerConsumerQueue<ISqlOperation> newQueue, MySqlBase<T> mySqlBase)
        {
            _queue = newQueue;
            _mySqlBase = mySqlBase;
            _cancelationToken = false;
            _workerThread = new Thread(WorkerThread);
            _workerThread.Start();
        }

        void WorkerThread()
        {
            if (_queue == null)
                return;

            for (; ; )
            {
                ISqlOperation operation;

                _queue.WaitAndPop(out operation);

                if (_cancelationToken || operation == null)
                    return;

                operation.Execute(_mySqlBase);
            }
        }
    }
}
