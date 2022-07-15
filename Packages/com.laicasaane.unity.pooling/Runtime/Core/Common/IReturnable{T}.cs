﻿namespace Unity.Pooling
{
    public interface IReturnable<T> where T : class
    {
        void Return(T instance);
    }
}
