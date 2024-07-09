using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.AddressableSystem;
using _Project.Scripts.AppEntryPoint;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities.ServiceLocator;


namespace _Project.Scripts.StatsAndBuffsSystem
{
    public interface IPlayerIconProvider
    {
        Sprite GetSpriteById(string id);
    }

    public class PlayerIconsProvider : IPlayerIconProvider, IInitializeble
    {
        private readonly Dictionary<string, Sprite> _sprites = new();

        private readonly string _tagName;

        public PlayerIconsProvider(string tagName)
        {
            _tagName = tagName;
        }

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            var addressablesService = ServiceLocator.Global.Get<IAddressableService>();
            var sprites = await addressablesService.LoadAssetsByTagAsync<Sprite>(_tagName, cancellationToken);

            Debug.Assert(sprites is { Count: > 0 }, "Failed to load sprites");

            foreach (var sprite in sprites)
            {
                _sprites.Add(sprite.name, sprite);
            }
        }

        public Sprite GetSpriteById(string id)
        {
            if (_sprites.TryGetValue(id, out var sprite))
            {
                return sprite;
            }

            Debug.LogWarning($"Sprite with id {id} not found.");
            return null;
        }
    }
}