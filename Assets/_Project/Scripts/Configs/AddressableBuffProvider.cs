using System.Collections.Generic;
using _Project.Scripts.AddressableSystem;
using _Project.Scripts.StatsSystem;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Configs
{
    public class AddressableBuffProvider : IBuffProvider
    {
        private readonly string _address;
        private readonly IAddressableService _addressableService;

        private IList<StatBuff> _buffs;

        public AddressableBuffProvider(string address, IAddressableService addressableService)
        {
            _address = address;
            _addressableService = addressableService;
        }

        public async UniTask LoadBuffsByTag(string address)
        {
            var buffSOs = await _addressableService.LoadAssetsByTagAsync<StatBuffSO>(address);
            _buffs = new List<StatBuff>();
            foreach (var buffSO in buffSOs)
            {
                _buffs.Add(buffSO.GetStatBuff());
            }
        }

        public IEnumerable<StatBuff> GetBuffs()
        {
            return _buffs;
        }
    }
}