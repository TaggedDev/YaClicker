using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DonateShop : ClickerCanvas, IPressableButton
    {
        public override CanvasLayer CanvasLayerTag => CanvasLayer.UraniumDonate;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private SaveLoader loader;

        private Dictionary<int, double> _rewards;
        public Dictionary<int, double> Rewards => _rewards;

        private void Start()
        {
            _rewards = new Dictionary<int, double>();
            Debug.Log("Initialized rewards");
            CanvasLayersController.Canvases.Add(this);
            gameObject.SetActive(false);
        }
        
        public void HandleButtonPress()
        {
            buttonText.rectTransform.anchoredPosition -= new Vector2(0, 20);
        }

        public void HandleButtonRelease()
        {
            buttonText.rectTransform.anchoredPosition += new Vector2(0, 20);
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.MainMenu);
        }

        public void UraniumShopOpen()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.UraniumShop);
        }

        public void GivePlayerReward(int rewardId)
        {
            loader.UraniumAmount.ResourceBank += Rewards[rewardId];
        }
    }
}