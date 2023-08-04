using UnityEngine;

namespace Economy
{
    /// <summary>
    /// Changes that provide bought upgrade
    /// </summary>
    [CreateAssetMenu(fileName = "DefaultUpgrade", menuName = "Upgrade", order = 1)]
    public class UpgradeMessage : ScriptableObject
    {
        [Header("Economy details")] 
        
        public double priceMultiplier = 0;
        public double price = 1;
        public double clickBonus = 0;
        public double autoClickBonus = 0;

        [HideInInspector] public uint upgradeLevel = 1;
        
        [Header("Visual details")]
        
        public Sprite upgradeIcon;
        public string descriptionText = "Default description text";
        public string levelText = "LVL: 0";
    }
}