using System.Collections.Generic;
using _Project.Scripts.AddressableSystem;
using _Project.Scripts.StatsSystem;
using UnityEngine;

namespace _Project.Scripts.Configs
{
    public class DataBuffProvider : IBuffProvider
    {
        private readonly TextAsset _dataAsset;
        private readonly IAddressableService _addressableService;
        private IList<StatBuff> _buffs;

        public DataBuffProvider(TextAsset dataAsset, IAddressableService addressableService)
        {
            _dataAsset = dataAsset;
            _addressableService = addressableService;
        }

        // public async UniTask LoadBuffsFromDataFile(string dataFileId)
        // {
        //     _buffs = new List<StatBuff>();
        //     
        //     Data oldDataFormat;
        //     try
        //     {
        //         var buffDataJson = await _addressableService.LoadAssetAsync<TextAsset>(dataFileId);
        //         
        //         oldDataFormat = JsonUtility.FromJson<Data>(buffDataJson.text);
        //
        //         if (oldDataFormat == null)
        //         {
        //             throw new Exception("Unable to parse data!!");
        //         }
        //     }
        //     catch (Exception ex) //Come up with some proper exception
        //     {
        //         Debug.LogError(ex.Message);
        //         throw;
        //     }
        //     
        //     foreach (var buff in oldDataFormat.buffs)
        //     {
        //         _buffs.Add(new StatBuff(buff.))
        //     }
        //     
        // }
        

        public IEnumerable<StatBuff> GetBuffs()
        {
            throw new System.NotImplementedException();
        }
    }
}