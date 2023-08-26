using UnityEngine;

namespace UI
{
    public class DonateCell : MonoBehaviour
    {
        [SerializeField] private UraniumDonatePosition donate;
        [SerializeField] private SaveLoader saveLoader;
        
        public void HandleAdWatch()
        {
            // TODO:
            // Watch AD -> Yandex API integration
            saveLoader.Resources[1].ResourceBank += donate.Reward;
            if (donate.BoostTime > 0)
                saveLoader.ApplyBoost(donate.BoostMultiplier, donate.BoostTime);
        }
    }
}