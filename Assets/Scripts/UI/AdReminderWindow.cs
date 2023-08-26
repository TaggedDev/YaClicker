using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class AdReminderWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Animator animator;

        public Animator Animator => animator;

        private void Start()
        {
            CloseReminder();
        }

        public void MoveToDonateCanvas()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.UraniumDonate);
            CloseReminder();
        }

        public void CloseReminder()
        {
            gameObject.SetActive(false);
        }
    }
}