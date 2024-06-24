using System;
using System.Threading;
using Bootstrap;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Data
{
    public class DataProviderFromAddressables
    {
        private readonly AddressablesService _addressablesService;
        private readonly string _address;

        public DataProviderFromAddressables(string address)
        {
            _addressablesService = ServiceLocator.GetService<AddressablesService>();
            _address = address;
        }

        public async UniTask<Data> LoadData(CancellationToken cancellationToken)
        {
            var text = await _addressablesService.LoadAssetAsync<TextAsset>(_address, cancellationToken);

            Debug.Assert(text != null, "Couldn't load Config");
            try
            {
                Data = JsonUtility.FromJson<Data>(text.text);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }

            Debug.Assert(Data != null, "Couldn't parse Config");
            return Data;
        }

        public Data Data { get; private set; }
    }
}