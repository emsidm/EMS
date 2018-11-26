using System;
using System.Collections;
using System.Collections.Generic;

namespace EMS.Contracts.Workers
{
    public class ObservableQueue<T> : IEnumerable<T>, IEnumerable, ICollection<T>, ICollection
    {
        private readonly Queue<T> _queue = new Queue<T>();

        public IEnumerator<T> GetEnumerator() => _queue.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            ItemAdded?.Invoke(this, null);
            _queue.Enqueue(item);
        }

        public void Enqueue(T item) => Add(item);

        public T Dequeue()
        {
            ItemRemoved?.Invoke(this, null);
            return _queue.Dequeue();
        }

        public T Peek() => _queue.Peek();

        public void Clear()
        {
            ItemRemoved?.Invoke(this, null);
            _queue.Clear();
        }

        public bool Contains(T item) => _queue.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _queue.CopyTo(array, arrayIndex);

        public bool Remove(T item) => false;

        public void CopyTo(Array array, int arrayIndex) => _queue.CopyTo(array as T[], arrayIndex);

        int ICollection.Count => _queue.Count;

        public bool IsSynchronized { get; }
        public object SyncRoot { get; }

        int ICollection<T>.Count => _queue.Count;

        public bool IsReadOnly => false;

        public event EventHandler ItemAdded;
        public event EventHandler ItemRemoved;
    }
}