using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using Yandex;

namespace UI
{
    public class BoostCell : MonoBehaviour, IPressableButton
    {
        [SerializeField] private SaveLoader saveLoader;
        [SerializeField] private UraniumBoostPosition boost;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private AdReminderWindow popUp;

        private Dictionary<string, string> _metricaMessage;

        private void Start()
        {
            buttonText.text = boost.Price.ToString();
            descriptionText.text = GenerateDescriptionText();
            _metricaMessage = new Dictionary<string, string>
            {
                { "ID", boost.ID }
            };

            string GenerateDescriptionText()
            {
                StringBuilder sb = new StringBuilder(boost.TitleText);
                
                sb.Append("\n<color=#05f254><size=36>");
                sb.Append(boost.DescriptionText);
                sb.Append("</size>");

                return sb.ToString();
            }
        }

        public void HandleBoostPurchase()
        {
            var balance = saveLoader.UraniumAmount.ResourceBank;
            if (balance < boost.Price)
            {
                // Offer visit shop
                popUp.gameObject.SetActive(true);
                popUp.Animator.SetTrigger("Pop");
                return;
            }
            saveLoader.UraniumAmount.ResourceBank -= boost.Price;
            saveLoader.ApplyBoost(boost.BoostMultiplier, boost.BoostTime);
            Metrica.SendMetricMessage("BoostBought", _metricaMessage);
        }

        public void HandleButtonPress()
        {
            buttonText.rectTransform.anchoredPosition -= new Vector2(0, 20);
        }

        public void HandleButtonRelease()
        {
            buttonText.rectTransform.anchoredPosition += new Vector2(0, 20);
            HandleBoostPurchase();
        }
    }
}