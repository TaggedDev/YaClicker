using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BoostCell : MonoBehaviour, IPressableButton
    {
        [SerializeField] private SaveLoader saveLoader;
        [SerializeField] private UraniumBoostPosition boost;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private AdReminderWindow popUp;
        
        private void Start()
        {
            buttonText.text = boost.Price.ToString();
            descriptionText.text = boost.DescriptionText;
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