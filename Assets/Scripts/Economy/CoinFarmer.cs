using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Economy
{
    /// <summary>
    /// Instance of coin source (one of three types)
    /// </summary>
    public class CoinFarmer : MonoBehaviour
    {
        [SerializeField] private SaveLoader saveLoader;
        [SerializeField] private ClickTextParent parent;

        [Header("Sunrays")] 
        [SerializeField] private Image boostFillBar; 
        [SerializeField] private Color boostLongColor;
        [SerializeField] private Color boostShortColor;
        [SerializeField] private Image longRays;
        [SerializeField] private Image shortRays;
        
        [Header("Objects To Hide")]
        [SerializeField] private Button shopButton;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image clickTriggerImage;
        private Color _defaultShortColor;
        private Color _defaultLongColor;

        private Animator _animator;
        private Image _farmerImage;
        private float _passiveIncomeCooldown = 1;
        private float _targetFarmSize;
        private float _currentIncomeMultiplier = 1;
        private IEnumerator _currentScalingCoroutine;
        private IEnumerator _currentBoostCountingCoroutine;
        private float _saveCooldown;

        private void Start()
        {
            _defaultLongColor = longRays.color;
            _defaultShortColor = shortRays.color;

            _farmerImage = GetComponent<Image>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _saveCooldown -= Time.deltaTime;
            if (_saveCooldown <= 0f)
            {
                SaveLoader.SaveProgress();
                _saveCooldown = 5f;
            }
            
            _passiveIncomeCooldown -= Time.deltaTime;
            if (_passiveIncomeCooldown > 0f)
                return;

            _passiveIncomeCooldown = 1;
            HandlePassiveIncome();
        }

        /// <summary>
        /// Adds passive points to balance
        /// </summary>
        private void HandlePassiveIncome() => ObtainResources(false, true);

        /// <summary>
        /// Adds points for one click
        /// </summary>
        public void HandleObjectClick()
        {
            ObtainResources(true, false);
            HandleClickAnimation();
        }

        private void HandleClickAnimation()
        {
            _animator.Play("FarmSqueeze");
        }

        /// <summary>
        /// Handles resources drop and add
        /// </summary>
        private void ObtainResources(bool showText, bool passiveIncome)
        {
            var income = saveLoader.CoinAmount.ResourcePerClick;
            if (passiveIncome)
                income = saveLoader.CoinAmount.ResourcePerAutoClick;

            income *= _currentIncomeMultiplier;
            saveLoader.CoinAmount.ResourceBank += income;
            var stringBalance = TranslateMoney(income);
            if (income != 0 && showText)
                parent.SpawnText(stringBalance);
        }

        public void SetActive(bool isActive)
        {
            backgroundImage.gameObject.SetActive(isActive);
            shopButton.interactable = isActive;
            _farmerImage.enabled = isActive;
            shortRays.enabled = isActive;
            longRays.enabled = isActive;
            clickTriggerImage.enabled = isActive;
        }

        /// <summary>
        /// Translates money from double type to readable format
        /// </summary>
        /// <param name="money">Amount of money to format</param>
        /// <returns>Readable version of money</returns>
        public static string TranslateMoney(double money)
        {
            // <0 -> 123
            // Thousands = K = 10^3
            // Millions = M = 10^6
            // Billions = B = 10^9
            // Trillions T = 10^12
            // Quadrillions = Q = 1^15
            // EXTRA = E18+
            var moneyString = money.ToString(CultureInfo.InvariantCulture);
            switch (money)
            {
                case >= 1000000000000000000:
                    var degree = CountDegree();

                    string CountDegree()
                    {
                        var answer = string.Empty;
                        for (int i = moneyString.IndexOf('+'); i < moneyString.Length; i++)
                            answer += moneyString[i];
                
                        return answer;
                    }

                    return $"{moneyString[0]}.{moneyString[2]}E{degree}";
                case >= 1000000000000000:
                    return $"{Math.Floor(money / 1000000000000000 * 10) / 10}Q";
                case >= 1000000000000:
                    return $"{Math.Floor(money / 1000000000000 * 10) / 10}T";
                case >= 1000000000:
                    return $"{Math.Floor(money / 1000000000 * 10) / 10}B";
                case >= 1000000:
                    return $"{Math.Floor(money / 1000000 * 10) / 10}M";
                case >= 1000:
                    return $"{Math.Floor(money / 1000 * 10) / 10}K";
                default:
                {
                    var decimalPart = (money - (int)money).ToString(CultureInfo.InvariantCulture);
                    char decimalFirstChar = decimalPart.Length > 2 ? decimalPart[2] : '0';


                    return money switch
                    {
                        >= 100 => $"{moneyString[0]}{moneyString[1]}{moneyString[2]}.{decimalFirstChar}",
                        >= 10 => $"{moneyString[0]}{moneyString[1]}.{decimalFirstChar}",
                        >= 1 => moneyString[0] + "." + decimalFirstChar,
                        _ => "0" + "." + decimalFirstChar
                    };
                }
            }
        }
        
        /// <summary>
        /// Handles duration of boost. Will be restarted on repeated call 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void HandleIncomeBoost(float multiplier, float secondsDuration)
        {
            if (_currentScalingCoroutine is not null)
            {
                StopCoroutine(_currentScalingCoroutine);
                StopCoroutine(_currentBoostCountingCoroutine);
            }
                
            
            _currentScalingCoroutine = StartTimer();
            StartCoroutine(_currentScalingCoroutine);
            
            IEnumerator StartTimer()
            {
                _currentBoostCountingCoroutine = EnableVisualBar();
                StartCoroutine(_currentBoostCountingCoroutine);

                IEnumerator EnableVisualBar()
                {
                    var animationTime = secondsDuration;
                    while (animationTime > 0)
                    {
                        animationTime -= Time.deltaTime;
                        boostFillBar.fillAmount = animationTime / secondsDuration;
                        yield return null;
                    }
                }


                _currentIncomeMultiplier = multiplier;
                longRays.color = boostLongColor;
                shortRays.color = boostShortColor;
                yield return new WaitForSeconds(secondsDuration);
                longRays.color = _defaultLongColor;
                shortRays.color = _defaultShortColor;
                _currentIncomeMultiplier = 1;
            }
        }
    }
}