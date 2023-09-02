using System;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Economy
{
    public enum ResourceType
    {
        Coin,
        Uranium
    }
    /// <summary>
    /// Model of resource player can earn
    /// </summary>
    public class PlayerResource : MonoBehaviour
    {
        [Header("Resource components cache")]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private Button donateButton;
        [SerializeField] private TextMeshProUGUI valueText; 
        
        [Header("Visual settings")]
        [SerializeField] private Color iconColor;
        [SerializeField] private Color backgroundColor;
        [SerializeField] private bool hasDonateButton;
        
        // Economic fields
        [Header("Economics")]
        [SerializeField] private double resourcePerClick;
        [SerializeField] private double resourcePerAutoClick;
        private double _resourceBank;

        public event EventHandler<double> OnResourceChanged = delegate { };
        
        public double ResourcePerAutoClick
        {
            get => resourcePerAutoClick;
            set => resourcePerAutoClick = value;
        }

        public double ResourcePerClick
        {
            get => resourcePerClick;
            set => resourcePerClick = value;
        }

        public double ResourceBank
        {
            get => _resourceBank;
            set
            {
                if (value < 0)
                    throw new ArithmeticException($"Setting value is below zero");

                _resourceBank = Math.Round(value, 3);

                OnResourceChanged(null, _resourceBank);
                var stringValue = CoinFarmer.TranslateMoney(_resourceBank);
                valueText.text = stringValue;
            }
        }
        
        public void HandleDonateButton()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.UraniumDonate);
        }

        private void OnValidate()
        {
            backgroundImage.color = backgroundColor;
            iconImage.color = iconColor;
            donateButton.gameObject.SetActive(hasDonateButton);
        }
    }
}
