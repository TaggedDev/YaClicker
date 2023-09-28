using TMPro;
using UnityEngine;

namespace UI
{
    public class AdReminderWindow : MonoBehaviour, IPressableButton
    {
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Animator animator;

        public Animator Animator => animator;

        private void Start()
        {
            CloseReminder();
        }
        
        public void HandleButtonPress()
        {
            buttonText.rectTransform.anchoredPosition -= new Vector2(0, 6);
        }

        public void HandleButtonRelease()
        {
            buttonText.rectTransform.anchoredPosition = Vector2.zero;
            CloseReminder();
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.UraniumDonate);
        }

        public void CloseReminder()
        {
            gameObject.SetActive(false);
        }
    }
}