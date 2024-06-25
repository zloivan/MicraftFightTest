using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Data
{
    public class AddressablesService : IAddressableService
    {
        private readonly Dictionary<string, object> _loadedAssets = new();

        // Load asset by address/ID with cancellation token
        public async UniTask<T> LoadAssetAsync<T>(string address, CancellationToken cancellationToken = default)
        {
            if (_loadedAssets.TryGetValue(address, out var asset))
            {
                return (T)asset;
            }

            var handle = Addressables.LoadAssetAsync<T>(address);
            await handle.Task.AsUniTask();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _loadedAssets[address] = handle.Result;
                return handle.Result;
            }

            Debug.LogError($"Failed to load asset at address: {address}");
            return default;
        }

        // Load assets by tag with cancellation token
        public async UniTask<IList<T>> LoadAssetsByTagAsync<T>(string tag, CancellationToken cancellationToken = default)
        {
            var handle = Addressables.LoadAssetsAsync<T>(tag, null);
            await handle.ToUniTask(cancellationToken: cancellationToken);

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var asset in handle.Result)
                {
                    _loadedAssets[asset.ToString()] = asset;
                }
                return handle.Result;
            }

            Debug.LogError($"Failed to load assets with tag: {tag}");
            return default;
        }

        // Unload asset by address/ID
        public void UnloadAsset(string address)
        {
            if (_loadedAssets.ContainsKey(address))
            {
                Addressables.Release(_loadedAssets[address]);
                _loadedAssets.Remove(address);
            }
            else
            {
                Debug.LogWarning($"No asset loaded at address: {address} to unload.");
            }
        }

        // Change scene by name with cancellation token
        public async UniTask ChangeSceneAsync(string sceneName, CancellationToken cancellationToken = default)
        {
            var handle = Addressables.LoadSceneAsync(sceneName);
            await handle.ToUniTask(cancellationToken: cancellationToken);

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Failed to load scene: {sceneName}");
            }
        }

        // Load scene additively by name with cancellation token
        public async UniTask LoadSceneAdditiveAsync(string sceneName, CancellationToken cancellationToken = default)
        {
            var handle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            await handle.ToUniTask(cancellationToken: cancellationToken);

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Failed to load scene additively: {sceneName}");
            }
        }
    }
}
