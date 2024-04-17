﻿using System;
using System.Threading;
using System.Runtime.CompilerServices;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace ZBase.Foundation.Pooling.UnityPools
{
    public abstract class PoolBehaviour<T, TPool> : MonoBehaviour, IAsyncPool<T>
        where T : class
        where TPool : IAsyncPool<T>
    {
        [SerializeField]
        private TPool _pool;

        public TPool Pool
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _pool;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count()
            => _pool.Count();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReleaseInstances(int keep, Action<T> onReleased = null)
            => _pool.ReleaseInstances(keep, onReleased);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async UniTask<T> Rent()
            => await _pool.Rent();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async UniTask<T> Rent(CancellationToken cancelToken)
            => await _pool.Rent(cancelToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Return(T instance)
            => _pool.Return(instance);

        protected virtual void OnDestroy()
        {
            if (_pool is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
