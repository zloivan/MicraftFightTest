using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.StatsSystem
{
    [CreateAssetMenu(fileName = "New Buff_DeBuff", menuName = "Stat System/Create New Buff", order = 0)]
    public class StatBuffSO : ScriptableObject
    {
        public string BuffName;
        public string BuffDescription;
        public List<StatBuffValue> StatsToChange;
        public float Duration;
        public AssetReferenceSprite IconAddress; //TODO use addressable address

        public string IconAddressString { get; private set; }

        private void OnValidate()
        {
            IconAddressString = IconAddress != null ? IconAddress.AssetGUID : string.Empty;
        }

        public StatBuff GetStatBuff()
        {
            Debug.Assert(!string.IsNullOrEmpty(IconAddressString), $"Address for {name} sprite must be defined",
                this);

            Debug.Assert(!string.IsNullOrEmpty(BuffName),$"Buff Name for {name} must be defined",this);

            Debug.Assert(StatsToChange is { Count: > 0 }, "Buffs modifiers values must be defined",
                this);
            
            return new StatBuff(StatsToChange, Duration, IconAddressString, BuffName, BuffDescription);
        }
    }

    public class StatBuff
    {
        public readonly string Name;

        public readonly List<StatBuffValue> StatsToChange;

        public readonly float Duration;

        public readonly string IconAddress;

        public readonly string Description;


        public StatBuff(List<StatBuffValue> statsToChange,
            float duration,
            string iconAddress,
            string name,
            string description)
        {
            StatsToChange = statsToChange;
            Duration = duration;
            IconAddress = iconAddress;
            Name = name;
            Description = description;
        }
    }
}