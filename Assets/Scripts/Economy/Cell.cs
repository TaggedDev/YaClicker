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
        
        public UpgradeMessage UpgradeMessage { get; private set; }

        public void AttachUpgradeToCell(UpgradeMessage message, CoinFarmer farmer)
        {
            UpgradeMessage = message;
            image.sprite = message.upgradeIcon;
            levelText.text = message.levelText;
            descriptionText.text = message.descriptionText;
            purchaseButton.onClick.AddListener(HandlePurchase);
            CoinFarmer.OnPointsChanged += UpdateBuyButtonCondition;
        }

        private void UpdateBuyButtonCondition(object sender, double e)
        {
            // If not enough money to buy upgrade -> disable button
            //throw new System.NotImplementedException();
        }


        public void HandlePurchase()
        {
            // Grant benefits
            // Subtract points
            // Increase price
            throw new System.NotImplementedException();
        }
    }
}