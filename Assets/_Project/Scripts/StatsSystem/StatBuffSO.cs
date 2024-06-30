using _Project.Scripts.PickupSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityExtensions;

namespace _Project.Scripts.StatsSystem
{
    [CreateAssetMenu(fileName = "New Buff_DeBuff", menuName = "Stat System/Create New Buff", order = 0)]
    public class StatBuffSO : ScriptableObject
    {
        public string BuffName;
        public string BuffDescription;
        public StatBuffContainer StatsToChange;
        public float Duration;
        public AssetReferenceSprite IconAddress; //TODO use addressable address


        public StatBuff GetStatBuff()
        {
            Debug.Assert(!string.IsNullOrEmpty(IconAddress.AssetGUID), $"Address for {name} sprite must be defined",
                this);

            Debug.Assert(!string.IsNullOrEmpty(BuffName), $"Buff Name for {name} must be defined", this);

            Debug.Assert(StatsToChange.ModifiersDescriptions is { Count: > 0 },
                "StatsToChange.ModifiersDescBuffs modifiers values must be defined",
                this);

            var factory = new StatBuffFactory();

            return factory.Create(StatsToChange, Duration, BuffName, BuffDescription, IconAddress.AssetGUID);
        }


        private void OnValidate()
        {
            if (BuffName.IsBlank())
            {
                BuffName = name;
            }
        }
    }
}