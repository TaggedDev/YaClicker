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
        [SerializeField] private double clickBonus = 0;
        [SerializeField] private double autoClickBonus = 0;

        [Header("Economy details")] 
        
        [SerializeField] private double priceDegreeModificator = 0;
        [SerializeField] private double startPrice;
        [Header("Visual details")] 
        
        [SerializeField] private uint upgradeID;
        [SerializeField] private Sprite upgradeIcon;
        [SerializeField] private string descriptionText = "Default description text";
        [SerializeField] private string levelText = "LVL: 0";

        public double StartPrice => startPrice;
        public double ClickBonus => clickBonus;
        public double AutoClickBonus => autoClickBonus;
        public double PriceDegreeModificator => priceDegreeModificator;
        public uint UpgradeID => upgradeID;
        public Sprite UpgradeIcon => upgradeIcon;
        public string DescriptionText => descriptionText;
        public string LevelText => levelText;
    }
}