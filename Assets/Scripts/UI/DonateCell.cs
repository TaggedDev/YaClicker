using TMPro;
using UnityEngine;
using YG;

namespace UI
{
    public class DonateCell : MonoBehaviour
    {
        [SerializeField] private UraniumDonatePosition donate;
        [SerializeField] private DonateShop donateShop;
        [SerializeField] private TextMeshProUGUI descriptionText;
        private void Start()
        {
            descriptionText.text = donate.DescriptionText;
            donateShop.Rewards.Add(donate.ID, donate.Reward);
            Debug.Log($"Added {donate.ID} in list");
        }

        public void HandleAdWatch()
        {
            YandexGame.RewVideoShow(donate.ID);
        }
    }
}