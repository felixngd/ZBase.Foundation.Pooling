﻿using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Unity.Pooling.AddressableAssets
{
    [Serializable]
    public class AssetRefComponentPrefab<T> : UnityPrefab<T, AssetReferenceGameObject>
        where T : Component
    {
        protected override async UniTask<T> InstantiateAsync(AssetReferenceGameObject source, Transform parent)
        {
            GameObject gameObject;

            if (parent)
                gameObject = await source.InstantiateAsync(parent, true);
            else
                gameObject = await source.InstantiateAsync();

            return gameObject.GetComponent<T>();
        }
    }
}
