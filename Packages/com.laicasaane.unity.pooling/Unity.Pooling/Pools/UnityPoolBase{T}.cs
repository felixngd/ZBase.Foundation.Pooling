﻿using System;
using System.Buffers;
using System.Pooling;
using Collections.Pooled.Generic;

namespace Unity.Pooling
{
    public abstract partial class UnityPoolBase<T> : IPool<T>, IDisposable
        where T : UnityEngine.Object
    {
        private readonly Func<T> _instantiate;
        private readonly Queue<T> _queue;

        public UnityPoolBase()
            : this(null, ArrayPool<T>.Shared)
        { }

        public UnityPoolBase(Func<T> instantiate)
            : this(instantiate, ArrayPool<T>.Shared)
        { }

        public UnityPoolBase(ArrayPool<T> pool)
            : this(null, pool)
        { }

        public UnityPoolBase(Func<T> instantiate, ArrayPool<T> pool)
        {
            _instantiate = instantiate ?? GetInstantiator();
            _queue = new Queue<T>(pool ?? ArrayPool<T>.Shared);
        }

        public int Count() => _queue.Count;

        public void Dispose()
        {
            _queue.Dispose();
        }

        /// <inheritdoc/>
        public void ReleaseInstances(int keep, Action<T> onReleased = null)
        {
            var countRemove = _queue.Count - keep;

            while (countRemove > 0)
            {
                var instance = _queue.Dequeue();
                onReleased?.Invoke(instance);
                countRemove--;
            }
        }

        public T Rent()
        {
            if (_queue.Count > 0)
                return _queue.Dequeue();

            return _instantiate();
        }

        public void Return(T instance)
        {
            if (instance is null)
                return;

            ReturnPreprocess(instance);
            _queue.Enqueue(instance);
        }

        protected abstract void ReturnPreprocess(T instance);

        protected abstract Func<T> GetInstantiator();
    }
}
