using System;
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
        
        private int _upgradeLevel;
        private UpgradeMessage _upgradeMessage;
        private CoinFarmer _farmer;
        private double _price;

        public void AttachUpgradeToCell(UpgradeMessage message, CoinFarmer coinFarmer)
        {
            _upgradeMessage = message;
            image.sprite = message.UpgradeIcon;
            levelText.text = message.LevelText;
            descriptionText.text = message.DescriptionText;
            _farmer = coinFarmer;

            // Bind button
            purchaseButton.onClick.AddListener(HandleUpgradePurchase);
            CoinFarmer.OnPointsChanged += UpdateBuyButtonCondition;

            UpdateUpgradeButton();
        }

        private void UpdateBuyButtonCondition(object sender, double balance)
        {
            // If not enough money to buy upgrade -> disable button
            purchaseButton.interactable = balance >= _price;
        }
        
        private void HandleUpgradePurchase()
        {
            // Grant benefits
            _farmer.PointsPerAutoClick += _upgradeMessage.AutoClickBonus;
            _farmer.PointsPerClick += _upgradeMessage.ClickBonus;
            
            // Subtract points
            _farmer.PointsBalance -= _price;
            
            // Update price
            _price = GeneratePrice();
            UpdateUpgradeButton();
        }

        /// <summary>
        /// Generates new price for shop item
        /// </summary>
        /// <returns>Rounded shop item's price</returns>
        private double GeneratePrice()
        {
            var decreasingCoefficient = 10.0;
            return Math.Round(Math.Pow(Math.E, _upgradeMessage.StartPrice + _upgradeMessage.PriceDegreeModificator * (_upgradeLevel - 1) / decreasingCoefficient), 3);
        }

        private void UpdateUpgradeButton()
        {
            // Update text & visual
            _upgradeLevel++;
            levelText.text = $"LVL {_upgradeLevel}";
            _price = GeneratePrice();
            buttonText.text = _price.ToString();
        }
    }
}