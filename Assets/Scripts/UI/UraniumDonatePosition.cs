using UnityEngine;

namespace UI
{
    /// <summary>
    /// Model for uranium donation
    /// </summary>
    [CreateAssetMenu(fileName = "Donate Option", menuName = "Uranium Donate", order = 1)]
    public class UraniumDonatePosition : ScriptableObject
    {
        [SerializeField] private double reward = 0;
        [SerializeField] private float boostTime = 0;
        [SerializeField] private float boostMultiplier = 1;

        /// <summary>
        /// Duration (s) of boost 
        /// </summary>
        public float BoostTime => boostTime;
        
        /// <summary>
        /// Boost income coefficient
        /// </summary>
        public float BoostMultiplier => boostMultiplier;
        
        /// <summary>
        /// Reward for watching add (uranium)
        /// </summary>
        public double Reward => reward;
    }
    
}