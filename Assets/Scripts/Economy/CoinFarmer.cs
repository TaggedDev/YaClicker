using System;
using TMPro;
using UI;
using UnityEngine;

namespace Economy
{
    /// <summary>
    /// Instance of coin source (one of three types)
    /// </summary>
    public class CoinFarmer : MonoBehaviour
    {
        [SerializeField] private int maxBuildingTier;

        [SerializeField] private HUDCanvas hudCanvas;
        // [sf] private shop shop;

        private double _pointsBalance = 0;
        private double _pointsPerClick = 1;
        private double _pointsPerSecond = 1;
        
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
            _pointsBalance += _pointsPerSecond;
        }

        private void OnMouseDown()
        {
            HandleObjectClick();
            //hudCanvas.UpdateBalance(_pointsBalance);
            print(_pointsBalance);
        }

        /// <summary>
        /// Adds points for one click
        /// </summary>
        private void HandleObjectClick() => _pointsBalance += _pointsPerClick;
    }
}