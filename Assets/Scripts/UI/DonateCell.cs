using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using Yandex;
using YG;

namespace UI
{
    public class DonateCell : MonoBehaviour
    {
        [SerializeField] private UraniumDonatePosition donate;
        [SerializeField] private DonateShop donateShop;
        [SerializeField] private TextMeshProUGUI descriptionText;
        private Dictionary<string, string> _metricaMessage;

        private void Start()
        {
            _metricaMessage = new Dictionary<string, string>
            {
                { "ID", $"{donate.ID}" }
            };
            descriptionText.text = GenerateDescriptionText();
            
            donateShop.Rewards.Add(donate.ID, donate.Reward);
            
            string GenerateDescriptionText()
            {
                StringBuilder sb = new StringBuilder(donate.DescriptionText);
                sb.Append("\n<color=#05f254><size=36>");
                sb.Append($"+{donate.Reward} УРАНА\n");
                sb.Append("</size>");
                return sb.ToString();
            }
        }

        public void HandleAdWatch()
        {
            YandexGame.RewVideoShow(donate.ID);
            Metrica.SendMetricMessage("AdWatch", _metricaMessage);
        }
    }
}