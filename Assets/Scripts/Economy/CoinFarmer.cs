using System;
using System.Collections;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI textPrefab;

        private float _halfWidth, _halfHeight;
        private Animator _animator;
        private Button _button;
        private Image _image;
        private float _passiveIncomeCooldown = 1;
        private float _targetFarmSize;

        private void Start()
        {
            _halfHeight = textParent.rect.height / 2;
            _halfWidth = textParent.rect.width / 2;
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
            ObtainResources(false);
        }

        /// <summary>
        /// Adds points for one click
        /// </summary>
        public void HandleObjectClick()
        {
            ObtainResources(true);
            HandleClickAnimation();
        }

        private IEnumerator SpawnClickText(double number)
        {
            var position = new Vector3(Random.Range(-_halfWidth, _halfHeight), Random.Range(-_halfHeight, _halfHeight), 0);
            var text = Instantiate(textPrefab, Vector3.zero, Quaternion.identity, textParent.transform);
            text.transform.localPosition = position;
            text.text = $"{number}";
            yield return new WaitForSeconds(1f);
            Destroy(text.gameObject);
        }

        private void HandleClickAnimation()
        {
            _animator.Play("FarmSqueeze");
        }

        /// <summary>
        /// Handles resources drop and add
        /// </summary>
        private void ObtainResources(bool showText)
        {
            for (int i = 0; i < ResourcesAmount; i++)
            {
                if (dropChances[i].Chance > Random.Range(0f, 1f))
                {
                    var balance = saveLoader.Resources[i].ResourcePerClick;
                    saveLoader.Resources[i].ResourceBank += balance;
                    if (balance != 0 && showText)
                        StartCoroutine(SpawnClickText(balance));
                }
            }
        }

        public void SetActive(bool isActive)
        {
            _button.interactable = isActive;
            _image.enabled = isActive;
        }
   }
}