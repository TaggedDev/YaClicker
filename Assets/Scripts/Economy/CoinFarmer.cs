using System;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Economy
{
    /// <summary>
    /// Instance of coin source (one of three types)
    /// </summary>
    public class CoinFarmer : MonoBehaviour
    {
        [SerializeField] private MainMenu mainMenu;
        public static event EventHandler<double> OnPointsChanged = delegate { };

        public double PointsPerClick
        {
            get => _pointsPerClick;
            set
            {
                print(value);
                _pointsPerClick = value;
            }
        }
        public double PointsPerAutoClick
        {
            get => _pointsPerAutoClick;
            set
            {
                print(value);
                _pointsPerAutoClick = value;
            }
        }
        public double PointsBalance
        {
            get => _pointsBalance;
            set
            {
                if (value < 0)
                    return;
                
                _pointsBalance = value;
                OnPointsChanged(null, _pointsBalance);
                if (mainMenu.enabled)
                    mainMenu.UpdateBalance(_pointsBalance, _pointsPerClick, _pointsPerAutoClick);
            }
        }
        
        private double _pointsBalance = 0;
        private double _pointsPerClick = .5;
        private double _pointsPerAutoClick = 0;
        private float _passiveIncomeCooldown = 1;

        private void Start()
        {
            // Setting balance to zero to force call UpdateBalance method & trigger update events
            PointsBalance = 0;
        }

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
        private void HandlePassiveIncome() => PointsBalance += PointsPerAutoClick;

        /// <summary>
        /// Adds points for one click
        /// </summary>
        public void HandleObjectClick() => PointsBalance += PointsPerClick;
    }
}