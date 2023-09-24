using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Economy
{
    public class ClickTextParent : MonoBehaviour
    {
        [SerializeField] private int maxTextOnScreen;
        [SerializeField] private ClickText textPrefab;
        [SerializeField] private RectTransform spawnArea;
        
        private ClickText[] _textPool;
        private Queue<ClickText> _freeTexts;
        private float _halfWidth;
        private float _halfHeight;

        public void ReturnEverythingInPool()
        {
            StopAllCoroutines();
            foreach (var text in _textPool)
                _freeTexts.Enqueue(text);
        }

        public void ReturnInPool(ClickText text)
        {
            _freeTexts.Enqueue(text);
        } 

        public void SpawnText(string number)
        {
            if (_freeTexts.Count == 0)
                return;
            
            var position = new Vector2(Random.Range(-_halfWidth, _halfWidth), Random.Range(-_halfHeight, _halfHeight));
            var nextText = _freeTexts.Dequeue();
            
            // Set up resource setting for click text
            nextText.RectTransform.anchoredPosition = position;
            nextText.Text.text = number;

            // Let the animation process and destroy object
            nextText.SpawnClickText();
        }

        private void Start()
        {
            _freeTexts = new Queue<ClickText>();

            for (int i = 0; i < maxTextOnScreen; i++)
            {
                var text = Instantiate(textPrefab, Vector3.zero, Quaternion.identity, transform);
                _freeTexts.Enqueue(text);
                text.Parent = this;
                text.gameObject.SetActive(false);
                print(_freeTexts.Count);
            }

            var rect = spawnArea.rect;
            _halfHeight = rect.height / 2;
            _halfWidth = rect.width / 2;
            
        }
    }
}