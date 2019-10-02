using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ImageToolkit.HyperDrive
{
    static class cHyperDrive
    {
        static cHyperDrive()
        {
            _processors = Environment.ProcessorCount;
            _executionQueue = new Queue<IHyperOperation>();
            _queueSyncLock = new Object();
        }

        private static Object _queueSyncLock;  // Used as critical section lock
        private static Queue<IHyperOperation> _executionQueue; // Pending operations queues 
        private static int _processors;  // Number of processors in the system
        private static int _threadCount = 0;  // Number of threads that were created for operations
        private static AutoResetEvent allComplete = null; // wait signal for main thread to sit on 

        public static int Processors
        {
            get { return _processors;  }
        }

        public static void AddTask(IHyperOperation op)
        {
            lock (_queueSyncLock)
            {
                _executionQueue.Enqueue(op);
            }
        }

        public static IHyperOperation GetTask()
        {
            IHyperOperation op = null;

            lock (_queueSyncLock)
            {
                if (_executionQueue.Count > 0)
                {
                    op = _executionQueue.Dequeue();
                }
                return op;
            }
        }

        public static void PerformTask(Object state)
        {
            IHyperOperation op = GetTask();

            while (op != null)
            {
                op.Execute();
                op = GetTask();
            }

            lock (_queueSyncLock)
            {
                _threadCount--;
                if (_threadCount <= 0)
                {
                    allComplete.Set();
                }
            }
        }

        public static void Run()
        {
            int availableThreads, IOPortThreads, numberToStart;
            allComplete = new AutoResetEvent(false);  // Get signal event for main thread
            _threadCount = 0;

            ThreadPool.GetAvailableThreads(out availableThreads, out IOPortThreads);

            numberToStart = Math.Min(availableThreads, _processors);
            for (int i = 0; i < numberToStart; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(PerformTask));
                _threadCount++;
            }
            allComplete.WaitOne();
            allComplete.Dispose();
            allComplete = null;
        }
    }
}
