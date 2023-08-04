using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Economy
{
    /// <summary>
    /// Model of cell in shop
    /// </summary>
    public class Cell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Image image;
        [SerializeField] private Button purchaseButton;
        private CoinFarmer _farmer;

        public UpgradeMessage UpgradeMessage { get; private set; }

        public void AttachUpgradeToCell(UpgradeMessage message, CoinFarmer coinFarmer)
        {
            UpgradeMessage = message;
            image.sprite = message.upgradeIcon;
            levelText.text = message.levelText;
            descriptionText.text = message.descriptionText;
            _farmer = coinFarmer;
            
            // Bind button
            purchaseButton.onClick.AddListener(HandlePurchase);
            CoinFarmer.OnPointsChanged += UpdateBuyButtonCondition;
        }

        private void UpdateBuyButtonCondition(object sender, double balance)
        {
            // If not enough money to buy upgrade -> disable button
            if (balance < UpgradeMessage.price)
                purchaseButton.interactable = false;
        }
        
        private void HandlePurchase()
        {
            // Grant benefits
            _farmer.PointsPerSecond += UpgradeMessage.autoClickBonus;
            _farmer.PointsPerClick += UpgradeMessage.clickBonus;
            // Subtract points
            _farmer.PointsBalance -= UpgradeMessage.price;
            // Increase price
            UpgradeMessage.price *= UpgradeMessage.priceMultiplier;
        }
    }
}