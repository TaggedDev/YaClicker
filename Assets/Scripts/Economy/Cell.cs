using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Yandex;

namespace Economy
{
    /// <summary>
    /// Model of cell in shop
    /// </summary>
    public class Cell : MonoBehaviour, IPressableButton
    {
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Image image;
        [SerializeField] private Button purchaseButton;
        [SerializeField] private EventTrigger buttonTrigger;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private RectTransform rectTransform;
        private int _upgradeLevel;
        private Dictionary<string, string> _metricaMessage;

        public int UpgradeLevel
        {
            get => _upgradeLevel;
            set
            {
                _upgradeLevel = value;
                UpdateCell();
            }
            
        }

        public RectTransform RectTransform => rectTransform;

        private Cell _previousCell;
        private int _resourceType;
        private UpgradeMessage _upgradeMessage;
        private SaveLoader _loader;
        private double _price;


        public void AttachUpgradeToCell(UpgradeMessage message, SaveLoader loader, Cell previousUpgradeCell)
        {
            _loader = loader;
            _upgradeMessage = message;
            image.sprite = message.UpgradeIcon;
            levelText.text = message.LevelText;
            descriptionText.text = GenerateDescriptionText();
            _previousCell = previousUpgradeCell;
            _metricaMessage = new Dictionary<string, string>()
            {
                { "upgradeID", $"{message.UpgradeID}"},
                { "level", "0"}
            };
            
            // Bind button
            BindButtonTriggers();
            loader.CoinAmount.OnResourceChanged += UpdateBuyButtonCondition;
            UpdateUpgradeButton();

            void BindButtonTriggers()
            {
                var pointerDown = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerDown
                };
                pointerDown.callback.AddListener(_ => HandleButtonPress());
                
                var pointerUp = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerUp
                };
                pointerUp.callback.AddListener(_ => HandleButtonRelease());
                
                buttonTrigger.triggers.Add(pointerDown);
                buttonTrigger.triggers.Add(pointerUp);
            }

            string GenerateDescriptionText()
            {
                StringBuilder sb = new StringBuilder(message.DescriptionText);
                
                sb.Append("\n<color=#ffa500ff><size=36>");
                sb.Append($"+{CoinFarmer.TranslateMoney(message.ClickBonus)} за клик\n");
                sb.Append($"+{CoinFarmer.TranslateMoney(message.AutoClickBonus)} за автоклик\n");
                sb.Append("</size>");

                return sb.ToString();
            }
        }

        private void UpdateBuyButtonCondition(object sender, double balance)
        {
            // If not enough money to buy upgrade -> disable button
            var isAffordable = balance >= _price;
            purchaseButton.interactable = isAffordable;
            
            if (_previousCell is null)
                return;

            var isPreviousUpgradeBought = _previousCell.UpgradeLevel > 1;
            
            // Check affordability 
            // Enable cell if: previous item was already bought or this item is affordable 
            var isPurchaseAvailable = isPreviousUpgradeBought || isAffordable;

            gameObject.SetActive(isPurchaseAvailable);
        }
        
        /// <summary>
        /// Handles upgrade button click
        /// </summary>
        private void HandleUpgradePurchase()
        {
            // Grant benefits
            _loader.CoinAmount.ResourcePerClick += _upgradeMessage.ClickBonus;
            _loader.CoinAmount.ResourcePerAutoClick += _upgradeMessage.AutoClickBonus;
            

            var oldPrice = _price;

            // Update price
            _price = GeneratePrice();
            UpdateUpgradeButton();
            
            // Subtract points and call <UpdateBuyButtonCondition> from ResourceBank
            _loader.CoinAmount.ResourceBank -= oldPrice;
            _metricaMessage["level"] = $"{UpgradeLevel}";
            Metrica.SendMetricMessage($"UpgradePurchase", _metricaMessage);
        }

        /// <summary>
        /// Generates new price for shop item
        /// </summary>
        /// <returns>Rounded shop item's price</returns>
        private double GeneratePrice()
        {
            // Math.Round(Math.Pow(Math.E, _upgradeMessage.StartPrice + _upgradeMessage.PriceDegreeModificator * (_upgradeLevel - 1) / decreasingCoefficient), 3);
            if (UpgradeLevel == 0)
                return _upgradeMessage.StartPrice;
            return Math.Round(_upgradeMessage.StartPrice * Math.Pow(_upgradeMessage.PriceDegreeModificator, UpgradeLevel - 1), 3);
        }

        private void UpdateUpgradeButton()
        {
            // Update text & visual
            UpgradeLevel++;
            UpdateCell();
        }

        private void UpdateCell()
        {
            levelText.text = $"LVL {UpgradeLevel}";
            _price = GeneratePrice();
            buttonText.text = CoinFarmer.TranslateMoney(_price);
        }

        public void HandleButtonPress()
        {
            if (!purchaseButton.interactable)
                return;
            
            buttonText.rectTransform.anchoredPosition -= new Vector2(0, 20);
        }

        public void HandleButtonRelease()
        {
            buttonText.rectTransform.anchoredPosition = new Vector2(0, 0);
            if (!purchaseButton.interactable)
                return;
            
            HandleUpgradePurchase();
        }
    }
}