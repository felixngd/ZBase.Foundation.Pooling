﻿using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Unity.Pooling
{
    public class ComponentPool<T> : UnityPoolBase<T>
        where T : UnityEngine.Component
    {
        public static readonly ComponentPool<T> Shared  = new ComponentPool<T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ReturnPreprocess(T instance)
        {
            if (instance.gameObject.activeSelf)
                instance.gameObject.SetActive(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override Func<T> GetDefaultInstantiator()
            => Instantiator;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T Instantiator()
        {
            var go = new GameObject(typeof(T).Name);
            var instance = go.AddComponent<T>();
            return instance;
        }
    }
}