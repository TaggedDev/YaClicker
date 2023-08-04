using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Economy
{
    /// <summary>
    /// Changes that provide bought upgrade
    /// </summary>
    [CreateAssetMenu(fileName = "DefaultUpgrade", menuName = "Upgrade", order = 1)]
    public class UpgradeMessage : ScriptableObject
    {
        [Header("Economy details")] 
        
        public double purchaseMultiplier = 0;
        public double purchaseCost = 1;
        public double clickBonus = 0;
        public double autoClickBonus = 0;
        
        [Header("Visual details")]
        
        public Sprite upgradeIcon;
        public string descriptionText = "Default description text";
        public string levelText = "Level: 0";
    }
}