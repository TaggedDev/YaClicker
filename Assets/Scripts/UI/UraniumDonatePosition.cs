using UnityEngine;

namespace UI
{
    /// <summary>
    /// Model for uranium donation
    /// </summary>
    [CreateAssetMenu(fileName = "Donate Option", menuName = "Uranium Donate", order = 1)]
    public class UraniumDonatePosition : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private double reward;
        [SerializeField] private string descriptionText;
        
        /// <summary>
        /// Reward for watching add (uranium)
        /// </summary>
        public double Reward => reward;

        public string DescriptionText => descriptionText;

        public int ID => id;
    }
    
}