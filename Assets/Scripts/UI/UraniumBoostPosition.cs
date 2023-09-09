using UnityEngine;

namespace UI
{
    /// <summary>
    /// Model for uranium donation
    /// </summary>
    [CreateAssetMenu(fileName = "Boost Option", menuName = "Boost menu", order = 1)]
    public class UraniumBoostPosition : ScriptableObject
    {
        [SerializeField] private double price = 0;
        [SerializeField] private float boostTime = 0;
        [SerializeField] private float boostMultiplier = 1;
        [SerializeField] private string titleText;
        [SerializeField] private string descriptionText;
        [SerializeField] private string id;
        
        public string ID => id;

        /// <summary>
        /// Duration (s) of boost 
        /// </summary>
        public float BoostTime => boostTime;
        
        /// <summary>
        /// Boost income coefficient
        /// </summary>
        public float BoostMultiplier => boostMultiplier;
        
        /// <summary>
        /// Boost price
        /// </summary>
        public double Price => price;

        public string DescriptionText => descriptionText;

        public string TitleText => titleText;
    }
    
}