using System;
using UI;
using UnityEngine;

namespace Economy
{
    /// <summary>
    /// Instance of coin source (one of three types)
    /// </summary>
    public class CoinFarmer : MonoBehaviour
    {
        [SerializeField] private HUDCanvas hudCanvas;

        public static event EventHandler<double> OnPointsChanged = delegate { };


        public double PointsPerClick { get; set; } = 1;
        public double PointsPerSecond { get; set; } = 1;
        public double PointsBalance
        {
            get => _pointsBalance;
            set
            {
                if (value < 0)
                    return;
                
                _pointsBalance = value;
                OnPointsChanged(null, _pointsBalance);
                if (hudCanvas.enabled)
                    hudCanvas.UpdateBalance(_pointsBalance);
            }
        }
        private double _pointsBalance = 0;

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
            PointsBalance += PointsPerSecond;
        }

        private void OnMouseDown()
        {
            HandleObjectClick();
        }

        /// <summary>
        /// Adds points for one click
        /// </summary>
        private void HandleObjectClick() => PointsBalance += PointsPerClick;
    }
}