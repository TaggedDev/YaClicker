using TMPro;
using UnityEngine;

namespace UI
{
    public class UraniumShop : ClickerCanvas, IPressableButton
    {
        public override CanvasLayer CanvasLayerTag => CanvasLayer.UraniumShop;
        [SerializeField] private TextMeshProUGUI buttonText;

        private void Start()
        {
            CanvasLayersController.Canvases.Add(this);
            gameObject.SetActive(false);
        }

        public void HandleButtonPress()
        {
            buttonText.rectTransform.anchoredPosition -= new Vector2(0, 20);
        }

        public void HandleButtonRelease()
        {
            buttonText.rectTransform.anchoredPosition = Vector2.zero;
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.MainMenu);
        }
    }
}