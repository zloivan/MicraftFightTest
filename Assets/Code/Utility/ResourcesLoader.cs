using UnityEngine;

namespace Code.Utility
{
    public class ResourcesLoader : IDataLoader
    {
        private readonly string _assetName;

        public ResourcesLoader(string assetName)
        {
            _assetName = assetName;

            LoadData();
        }

        public Data.Data Data { get; private set; }

        private void LoadData()
        {
            var textAsset = Resources.Load<TextAsset>(_assetName);
            if (textAsset != null)
            {
                Data = JsonUtility.FromJson<Data.Data>(textAsset.text);
            }
            else
            {
                Debug.LogError($"Cannot find data file in Resources at path: {_assetName}");
            }
        }
    }
}