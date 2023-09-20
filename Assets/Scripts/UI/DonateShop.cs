using System.Collections.Generic;
using Economy;
using TMPro;
using UnityEngine;
using YG;

namespace UI
{
    public class DonateShop : ClickerCanvas, IPressableButton
    {
        public override CanvasLayer CanvasLayerTag => CanvasLayer.UraniumDonate;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private PlayerResource uranium;
        [SerializeField] private DonateCell[] cells;

        private Dictionary<int, double> _rewards;
        public Dictionary<int, double> Rewards => _rewards;

        private void OnEnable() => YandexGame.RewardVideoEvent += HandleRewardAdWatch;
        private void OnDisable() => YandexGame.RewardVideoEvent -= HandleRewardAdWatch;

        private void HandleRewardAdWatch(int rewardId)
        {
            uranium.ResourceBank += Rewards[rewardId];
            ShowDoubleCell(rewardId);
            
            SaveLoader.SaveProgress();
        }

        private void ShowDoubleCell(int rewardId)
        {
            if (rewardId == 2)
            {
                HideDonateOptions();
                return;
            }
            
            cells[rewardId+1].gameObject.SetActive(true);
        }

        private void Start()
        {
            _rewards = new Dictionary<int, double>();
            Debug.Log("Initialized rewards");
            CanvasLayersController.Canvases.Add(this);
            gameObject.SetActive(false);

            HideDonateOptions();
        }

        private void HideDonateOptions()
        {
            for (int i = 1; i < cells.Length; i++)
                cells[i].gameObject.SetActive(false);
        }

        public void HandleButtonPress()
        {
            buttonText.rectTransform.anchoredPosition -= new Vector2(0, 20);
        }

        public void HandleButtonRelease()
        {
            buttonText.rectTransform.anchoredPosition += new Vector2(0, 20);
            HideDonateOptions();
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.MainMenu);
        }

        public void UraniumShopOpen()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.UraniumShop);
        }
    }
}