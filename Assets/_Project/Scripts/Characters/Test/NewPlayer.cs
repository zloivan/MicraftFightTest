using _Project.Scripts.AddressableSystem;
using _Project.Scripts.Configs;
using _Project.Scripts.ServiceLocatorSystem;
using _Project.Scripts.StatsSystem;
using UnityEngine;
using Logger = Utilities.Logger;

namespace _Project.Scripts.Characters.Test
{
    public class BuffApplierTest : MonoBehaviour
    {
        [SerializeField]
        private Entity _entity;

        [SerializeField]
        private Transform _buffIconParent;

        [SerializeField]
        private GameObject _buffIconPrefab;

        private IBuffProvider _buffProvider;

        private IAddressableService _addressableService;

        private void Start()
        {
            _buffProvider = ServiceLocator.For(this).Get<IBuffProvider>();
            _addressableService = ServiceLocator.For(this).Get<IAddressableService>();
        }

        [ContextMenu("Apply Buffs")]
        public void ApplyBuffs()
        {
            var buffsToApplie = _buffProvider.GetBuffs();

            BuffApplier.ApplyBuffs(_entity, buffsToApplie);

            Debug.Log($"{_entity.Stats}");
        }


        public void ApplyStats()
        {
            // foreach (var modifier in _entity.Stats.Mediator.ListModifiers)
            // {
            //     modifier.
            // }
        }


        [ContextMenu("Reset to Base")]
        public void TestResetStats()
        {
            _entity.Stats.Mediator.ClearBuffs();
            Debug.Log($"{_entity.Stats}");
        }

        [ContextMenu("Output buffs")]
        public void Output()
        {
            Logger.Log($"{string.Join(' ', _entity.Stats.Mediator.ActiveBuffs)}", Color.green);
        }
    }
}