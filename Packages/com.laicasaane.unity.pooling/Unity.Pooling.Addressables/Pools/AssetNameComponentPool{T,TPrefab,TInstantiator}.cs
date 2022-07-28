﻿using System;
using System.Pooling;
using UnityEngine;

namespace Unity.Pooling.AddressableAssets
{
    [Serializable]
    public class AssetNameComponentPool<T, TPrefab, TInstantiator>
        : ComponentPool<T, TPrefab>
        where T : Component
        where TPrefab : AssetNameComponentPrefab<T, TInstantiator>
        where TInstantiator : IAssetNameInstantiator<T>, new()
    {
        public AssetNameComponentPool()
            : base()
        { }

        public AssetNameComponentPool(TPrefab prefab)
            : base(prefab)
        { }

        public AssetNameComponentPool(UniqueQueue<int, T> queue, TPrefab prefab)
            : base(queue, prefab)
        { }
    }
}
