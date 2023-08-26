using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DonateCell : MonoBehaviour, IPressableButton
    {
        [SerializeField] private UraniumDonatePosition donate;
        [SerializeField] private SaveLoader saveLoader;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI buttonText;

        private void Start()
        {
            descriptionText.text = donate.DescriptionText;
        }

        private void HandleAdWatch()
        {
            // TODO:
            // Watch AD -> Yandex API integration
            // Wait -> add money
            saveLoader.Resources[1].ResourceBank += donate.Reward;
        }

        public void HandleButtonPress()
        {
            buttonText.rectTransform.anchoredPosition -= new Vector2(0, 20);
        }

        public void HandleButtonRelease()
        {
            buttonText.rectTransform.anchoredPosition += new Vector2(0, 20);
            HandleAdWatch();
        }
    }
}