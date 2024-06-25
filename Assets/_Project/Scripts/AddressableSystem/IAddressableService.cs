using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Data
{
    public interface IAddressableService
    {
        UniTask<T> LoadAssetAsync<T>(string address, CancellationToken cancellationToken = default);
        UniTask<IList<T>> LoadAssetsByTagAsync<T>(string tag, CancellationToken cancellationToken = default);
        UniTask LoadSceneAdditiveAsync(string sceneName, CancellationToken cancellationToken = default);
    }
}