using System.IO;
using UnityEngine;

namespace Code.Utility
{
    public class StreamingAssetsDataLoader : IDataLoader
    {
        private readonly string _dataFilePath;

        public StreamingAssetsDataLoader(string dataFilePath)
        {
            _dataFilePath = dataFilePath;

            LoadData();
        }

        public Data.Data Data { get; private set; }


        private void LoadData()
        {
            var filePath = Path.Combine(Application.streamingAssetsPath, _dataFilePath);
            if (File.Exists(filePath))
            {
                var dataAsJson = File.ReadAllText(filePath);
                Data = JsonUtility.FromJson<Data.Data>(dataAsJson);
            }
            else
            {
                Debug.LogError("Cannot find data file!");
            }
        }
    }
}