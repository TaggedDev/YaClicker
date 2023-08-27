using System;
using System.Linq;
using System.Text;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        [SerializeField] private ResourceType type;
        [SerializeField] private RectTransform rectTransform;
        
        public int UpgradeLevel => _upgradeLevel;
        public RectTransform RectTransform => rectTransform;

        private string[] _phrases = new[] { "Монет", "Урана", "Мощи", "Железа", "Кобальта", "Золота" };
        private Cell _previousCell;
        private int _resourceType;
        private int _upgradeLevel;
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
            _resourceType = (int)type;
            _previousCell = previousUpgradeCell;

            // Bind button
            BindButtonTriggers();
            loader.Resources[_resourceType].OnResourceChanged += UpdateBuyButtonCondition;
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
                
                sb.Append("\n<color=#ffa500ff><size=24>");
                sb.Append($"+{DoubleArrayToString(message.ClickBonus)} за клик\n");
                sb.Append($"+{DoubleArrayToString(message.AutoClickBonus)} за автоклик\n");
                sb.Append("</size><b><size=30>");

                string DoubleArrayToString(double[] array)
                {
                    if (array.Length == 0)
                        return string.Empty;

                    var arraySb = new StringBuilder();
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (array[i] == 0.0)
                            continue;
                        
                        arraySb.Append(CoinFarmer.TranslateMoney(array[i]));
                        arraySb.Append(" ");
                        arraySb.Append(_phrases[i]);
                        arraySb.Append("/");
                    }

                    return arraySb.ToString();
                }
                
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
            for (int i = 0; i < _loader.Resources.Length; i++)
            {
                _loader.Resources[i].ResourcePerClick += _upgradeMessage.ClickBonus[i];
                _loader.Resources[i].ResourcePerAutoClick += _upgradeMessage.AutoClickBonus[i];
            }

            var oldPrice = _price;

            // Update price
            _price = GeneratePrice();
            UpdateUpgradeButton();
            
            // Subtract points and call <UpdateBuyButtonCondition> from ResourceBank
            _loader.Resources[(int)type].ResourceBank -= oldPrice;
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
            buttonText.text = CoinFarmer.TranslateMoney(_price);
            buttonText.text += "\n" + "<size=20>" + GetCurrency() + "</size>";

            string GetCurrency()
            {
                switch (_upgradeMessage.UpgradePrice)
                {
                    case ResourceType.Coins:
                        return "[МОНЕТЫ]";
                    case ResourceType.Uranium:
                    case ResourceType.Power:
                        break;
                    case ResourceType.Iron:
                        return "[ЖЕЛЕЗО]";
                    case ResourceType.Cobalt:
                        return "[КОБАЛЬТ]";
                    case ResourceType.Gold:
                        return "[ЗОЛОТО]";
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return string.Empty;
            }
            
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