using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ResourceType
{
    Coins,
    Uranium,
    Power,
    Iron,
    Cobalt, 
    Gold,
}

namespace Economy
{
    public class PlayerResource : MonoBehaviour
    {
        [SerializeField] private ResourceType resourceType;
        
        [Header("Resource components cache")]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private Button donateButton;
        [SerializeField] private TextMeshProUGUI valueText; 
        
        [Header("Visual settings")]
        [SerializeField] private Color iconColor;
        [SerializeField] private Color backgroundColor;
        [SerializeField] private bool hasDonateButton;

        public ResourceType ResourceType => resourceType;
        public TextMeshProUGUI ValueText => valueText;
        
        private void OnValidate()
        {
            backgroundImage.color = backgroundColor;
            iconImage.color = iconColor;
            donateButton.gameObject.SetActive(hasDonateButton);
        }
    }
}
