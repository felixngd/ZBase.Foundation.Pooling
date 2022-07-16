﻿namespace System.Pooling
{
    public interface IPool<T>
        : IPool, IRentable<T>, IReturnable<T>, ICountable
        where T : class
    {
        /// <summary>
        /// Keeps the specified quantity and releases the pooled instances.
        /// </summary>
        /// <param name="keep"> Quantity that keep pooled instances. </param>
        void ReleaseInstances(int keep, Action<T> onReleased = null);
    }
}
