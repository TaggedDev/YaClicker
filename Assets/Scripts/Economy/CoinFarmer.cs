using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Economy
{
    /// <summary>
    /// Model of resource drop chance
    /// </summary>
    [Serializable]
    public class DropChance
    {
        [Range(0, 1), SerializeField] private float chance;
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
        public const int ResourcesAmount = 6;

        [SerializeField] private SaveLoader saveLoader;
        [SerializeField] private DropChance[] dropChances;
        [SerializeField] private RectTransform textParent;
        [SerializeField] private ClickText textPrefab;
        [SerializeField] private Color[] resourceColors;
        [SerializeField] private Sprite[] resourceSprites;

        [Header("Sunrays")] 
        [SerializeField] private Image boostFillBar; 
        [SerializeField] private Color boostLongColor;
        [SerializeField] private Color boostShortColor;
        [SerializeField] private Image longRays;
        [SerializeField] private Image shortRays;
        private Color _defaultShortColor;
        private Color _defaultLongColor;

        private float _halfWidth, _halfHeight;
        private Animator _animator;
        private Button _button;
        private Image _image;
        private float _passiveIncomeCooldown = 1;
        private float _targetFarmSize;
        private float _currentIncomeMultiplier = 1;
        private IEnumerator _currentScalingCoroutine;
        private IEnumerator _currentBoostCountingCoroutine;
        private static readonly int IsBoosted = Animator.StringToHash("IsBoosted");
        

        private void Start()
        {
            var rect = textParent.rect;
            _halfHeight = rect.height / 2;
            _halfWidth = rect.width / 2;

            _defaultLongColor = longRays.color;
            _defaultShortColor = shortRays.color;

            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _animator = GetComponent<Animator>();

            if (dropChances.Length != ResourcesAmount)
                throw new ArgumentException("Drop Chances has length other than ResourcesAmount in CoinFarmer");
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
        private void HandlePassiveIncome()
        {
            ObtainResources(false, true);
        }

        /// <summary>
        /// Adds points for one click
        /// </summary>
        public void HandleObjectClick()
        {
            ObtainResources(true, false);
            HandleClickAnimation();
        }

        private IEnumerator SpawnClickText(string number, int resourceIndex)
        {
            var position = new Vector2(Random.Range(-_halfWidth, _halfWidth), Random.Range(-_halfHeight, _halfHeight));
            var textInstance = Instantiate(textPrefab, Vector3.zero, Quaternion.identity, textParent.transform);
            
            // Set up resource setting for click text
            textInstance.RectTransform.anchoredPosition = position;
            textInstance.Text.text = number;
            textInstance.Text.color = resourceColors[resourceIndex];
            textInstance.Image.sprite = resourceSprites[resourceIndex];
            
            // Let the animation process and destroy object
            yield return new WaitForSeconds(1f);
            Destroy(textInstance.gameObject);
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
            for (int i = 0; i < ResourcesAmount; i++)
            {
                if (dropChances[i].Chance > Random.Range(0f, 1f))
                {
                    var income = saveLoader.Resources[i].ResourcePerClick;
                    if (passiveIncome)
                         income = saveLoader.Resources[i].ResourcePerAutoClick;

                    income *= _currentIncomeMultiplier;
                    saveLoader.Resources[i].ResourceBank += income;
                    var stringBalance = TranslateMoney(income);
                    if (income != 0 && showText)
                        StartCoroutine(SpawnClickText(stringBalance, i));
                }
            }
        }

        public void SetActive(bool isActive)
        {
            _button.interactable = isActive;
            _image.enabled = isActive;
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