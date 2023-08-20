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
        [SerializeField] private ResourceType type;

        private int _resourceType;
        private int _upgradeLevel;
        private UpgradeMessage _upgradeMessage;
        private SaveLoader _loader;
        private double _price;

        public void AttachUpgradeToCell(UpgradeMessage message, SaveLoader loader)
        {
            _loader = loader;
            _upgradeMessage = message;
            image.sprite = message.UpgradeIcon;
            levelText.text = message.LevelText;
            descriptionText.text = message.DescriptionText;
            _resourceType = (int)type;

            // Bind button
            purchaseButton.onClick.AddListener(HandleUpgradePurchase);
            loader.Resources[_resourceType].OnResourceChanged += UpdateBuyButtonCondition;
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
            for (int i = 0; i < _loader.Resources.Length; i++)
            {
                _loader.Resources[i].ResourcePerClick += _upgradeMessage.ClickBonus[i];
                _loader.Resources[i].ResourcePerAutoClick += _upgradeMessage.AutoClickBonus[i];
            }
            
            // Subtract points and call <UpdateBuyButtonCondition> from ResourceBank
            _loader.Resources[(int)type].ResourceBank -= _price;
            
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
            // Math.Round(Math.Pow(Math.E, _upgradeMessage.StartPrice + _upgradeMessage.PriceDegreeModificator * (_upgradeLevel - 1) / decreasingCoefficient), 3);
            if (_upgradeLevel == 0)
                return _upgradeMessage.StartPrice;
            return Math.Round(_upgradeMessage.StartPrice * Math.Pow(_upgradeMessage.PriceDegreeModificator, _upgradeLevel - 1), 3);
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