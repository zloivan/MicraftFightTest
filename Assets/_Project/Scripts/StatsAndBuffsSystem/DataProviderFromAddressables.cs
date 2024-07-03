using System;
using System.Threading;
using _Project.Scripts.AddressableSystem;
using _Project.Scripts.AppEntryPoint;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ServiceLocator = _Project.Scripts.ServiceLocatorSystem.ServiceLocator;

namespace _Project.Scripts.StatsAndBuffsSystem
{
    public interface IConfigProvider
    {
        Data Data { get; }
    }
    
    public class ConfigProviderFromAddressables : IConfigProvider, IInitializeble
    {
        private readonly string _address;
        public Data Data { get; private set; }

        public ConfigProviderFromAddressables(string address)
        {
            _address = address;
        }

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            var addressablesService = ServiceLocator.Global.Get<IAddressableService>();
            var text = await addressablesService.LoadAssetAsync<TextAsset>(_address, cancellationToken);
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
        }
    }
}