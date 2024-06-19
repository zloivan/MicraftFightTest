using System.Collections.Generic;
using UnityEngine;

namespace Code.Utility
{
    public class ImageProvider
    {
        private Dictionary<string, Sprite> _sprites;

        public ImageProvider(string resourcePath)
        {
            LoadSprites(resourcePath);
        }

        private void LoadSprites(string resourcePath)
        {
            _sprites = new Dictionary<string, Sprite>();
            var loadedSprites = Resources.LoadAll<Sprite>(resourcePath);

            foreach (var sprite in loadedSprites)
            {
                _sprites[sprite.name] = sprite;
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