using System.Collections;
using TMPro;
using UnityEngine;

namespace Economy
{
    public class ClickText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Animator animator;
        private ClickTextParent _parent;

        public RectTransform RectTransform => rectTransform;
        public TextMeshProUGUI Text => text;
        public ClickTextParent Parent { get; set;}

        public void SpawnClickText()
        {
            gameObject.SetActive(true);
            animator.SetTrigger("SpawnAnimation");
            StartCoroutine(HideInOneSecond());

            IEnumerator HideInOneSecond()
            {
                yield return new WaitForSeconds(1);
                gameObject.SetActive(false);
                Parent.ReturnInPool(this);
            }
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        
        
    }
}