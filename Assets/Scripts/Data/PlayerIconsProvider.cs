using System.Collections.Generic;
using System.Threading;
using Bootstrap;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Data
{
    public class PlayerIconsProvider
    {
        private readonly Dictionary<string, Sprite> _sprites = new();

        private readonly string _tagName;
        private readonly AddressablesService _addressablesService;

        public PlayerIconsProvider(string tagName)
        {
            _tagName = tagName;
            _addressablesService = ServiceLocator.GetService<AddressablesService>();
        }

        public async UniTask LoadSprites(CancellationToken cancellationToken)
        {
            var sprites = await _addressablesService.LoadAssetsByTagAsync<Sprite>(_tagName, cancellationToken);

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