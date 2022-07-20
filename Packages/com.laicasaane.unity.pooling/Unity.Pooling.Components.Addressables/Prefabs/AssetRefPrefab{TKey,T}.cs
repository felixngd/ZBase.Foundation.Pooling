﻿using System;
using UnityEngine.AddressableAssets;

namespace Unity.Pooling.Components.AddressableAssets
{
    [Serializable]
    public abstract class AssetRefPrefab<TKey, T, TAssetRef> : AddressablePrefab<TKey, TAssetRef>
        where T : UnityEngine.Object
        where TAssetRef : AssetReferenceT<T>
    {
    }
}
