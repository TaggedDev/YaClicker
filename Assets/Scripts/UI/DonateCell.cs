using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DonateCell : MonoBehaviour
    {
        [SerializeField] private UraniumDonatePosition donate;
        [SerializeField] private SaveLoader saveLoader;
        [SerializeField] private TextMeshProUGUI descriptionText;

        private void Start()
        {
            descriptionText.text = donate.DescriptionText;
        }

        public void HandleAdWatch()
        {
            // TODO:
            // Watch AD -> Yandex API integration
            // Wait -> add money
            saveLoader.UraniumAmount.ResourceBank += donate.Reward;
        }
    }
}