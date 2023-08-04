using System;
using System.Globalization;
using TMPro;
using UnityEngine;
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
        [SerializeField] private TextMeshProUGUI buttonText;
        private CoinFarmer _farmer;

        private UpgradeMessage UpgradeMessage { get; set; }

        public void AttachUpgradeToCell(UpgradeMessage message, CoinFarmer coinFarmer)
        {
            UpgradeMessage = message;
            image.sprite = message.upgradeIcon;
            levelText.text = message.levelText;
            descriptionText.text = message.descriptionText;
            _farmer = coinFarmer;

            // Bind button
            purchaseButton.onClick.AddListener(HandleUpgradePurchase);
            CoinFarmer.OnPointsChanged += UpdateBuyButtonCondition;
        }

        private void UpdateBuyButtonCondition(object sender, double balance)
        {
            // If not enough money to buy upgrade -> disable button
            purchaseButton.interactable = balance >= UpgradeMessage.price;
        }
        
        private void HandleUpgradePurchase()
        {
            // Grant benefits
            _farmer.PointsPerSecond += UpgradeMessage.autoClickBonus;
            _farmer.PointsPerClick += UpgradeMessage.clickBonus;
            // Subtract points
            _farmer.PointsBalance -= UpgradeMessage.price;
            // Increase price
            UpgradeMessage.price = Math.Round(UpgradeMessage.priceMultiplier * UpgradeMessage.price, 3);
            // Visual changes
            buttonText.text = UpgradeMessage.price.ToString();
            levelText.text = $"LVL {UpgradeMessage.upgradeLevel}";
            UpgradeMessage.upgradeLevel++;
        }
    }
}