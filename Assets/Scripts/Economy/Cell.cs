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

        public void AttachUpgradeToCell(UpgradeMessage message)
        {
            UpgradeMessage = message;
            image.sprite = message.upgradeIcon;
            levelText.text = message.levelText;
            descriptionText.text = message.descriptionText;
            purchaseButton.onClick.AddListener(HandlePurchase);
        }

        public void HandlePurchase()
        {
            
        }
    }
}