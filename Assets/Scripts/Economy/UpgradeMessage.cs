using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Economy
{
    /// <summary>
    /// Changes that provide bought upgrade
    /// </summary>
    [CreateAssetMenu(fileName = "DefaultUpgrade", menuName = "Upgrade", order = 1)]
    public class UpgradeMessage : ScriptableObject
    {
        [Header("Bonus details")]
        [Tooltip("Coins, Uranium, Power, Iron, Cobalt, Gold")] 
        [SerializeField] private double[] clickBonus;
        [Tooltip("Coins, Uranium, Power, Iron, Cobalt, Gold")] 
        [SerializeField] private double[] autoClickBonus;


        [Header("Economy details")]
        [SerializeField] private double priceDegreeModificator;
        [SerializeField] private double startPrice;
        [SerializeField] private ResourceType upgradePrice;

        [Header("Visual details")]
        [SerializeField] private uint upgradeID;
        [SerializeField] private Sprite upgradeIcon;
        [SerializeField] private string descriptionText = "Default description text";
        [SerializeField] private string levelText = "LVL: 0";

        public double StartPrice => startPrice;
        public double[] ClickBonus => clickBonus;
        public double[] AutoClickBonus => autoClickBonus;
        public double PriceDegreeModificator => priceDegreeModificator;
        public uint UpgradeID => upgradeID;
        public Sprite UpgradeIcon => upgradeIcon;
        public string DescriptionText => descriptionText;
        public string LevelText => levelText;
    }
}