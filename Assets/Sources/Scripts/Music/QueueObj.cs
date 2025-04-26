using System.Collections.Generic;

namespace CMSR.EnemySystem
{
    public class QueueObj<T>
    {
        private Queue<T> _queue;
        private T[] _ts;

        public QueueObj(T[] ts)
        {
            _ts = ts;
            InitQueue();
        }

        private void InitQueue()
        {
            _queue = new Queue<T>();

            foreach (var t in _ts)
            {
                _queue.Enqueue(t);
            }
        }

        public T Get()
        {
            if (_queue.Count > 0)
            {
                return _queue.Dequeue();
            }

            InitQueue();
            return _queue.Dequeue();
        }
    }
}