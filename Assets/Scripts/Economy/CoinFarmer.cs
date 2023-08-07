using System;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Economy
{
    /// <summary>
    /// Model of resource drop chance
    /// </summary>
    [Serializable]
    public class DropChance
    {
        [Range(0, 1)] [SerializeField] private float chance;
        [SerializeField] private ResourceType resourceType;
        
        /// <summary>
        /// Resource drop chance [0, 1] 
        /// </summary>
        public float Chance => chance;
        /// <summary>
        /// Resource type
        /// </summary>
        public ResourceType ResourceType => resourceType;
    }

    /// <summary>
    /// Instance of coin source (one of three types)
    /// </summary>
    public class CoinFarmer : MonoBehaviour
    {
        private const int ResourcesAmount = 6;
        
        [SerializeField] private SaveLoader saveLoader;
        [SerializeField] private DropChance[] dropChances;

        private float _passiveIncomeCooldown = 1;

        private void Update()
        {
            _passiveIncomeCooldown -= Time.deltaTime;
            if (_passiveIncomeCooldown > 0)
                return;

            _passiveIncomeCooldown = 1;
            HandlePassiveIncome();
        }

        /// <summary>
        /// Adds passive points to balance
        /// </summary>
        private void HandlePassiveIncome()
        {
            ObtainResources();
        }

        /// <summary>
        /// Adds points for one click
        /// </summary>
        public void HandleObjectClick()
        {
            ObtainResources();
        }

        private void ObtainResources()
        {
            for (int i = 0; i < ResourcesAmount; i++)
                if (dropChances[i].Chance > Random.Range(0, 1))
                    saveLoader.Resources[i].ResourceBank += saveLoader.Resources[i].ResourcePerClick;
        }
    }
}