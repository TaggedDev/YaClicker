using TMPro;
using UnityEngine;

namespace UI
{
    public interface IPressableButton
    {
        public void HandleButtonPress();
        public void HandleButtonRelease();
    }
    
    public class MainMenu : ClickerCanvas, IPressableButton
    {
        [Tooltip("Shop button text"),SerializeField] private TextMeshProUGUI buttonText;
        public override CanvasLayer CanvasLayerTag => CanvasLayer.MainMenu;
        
        public void HandleButtonPress()
        {
            buttonText.rectTransform.anchoredPosition -= new Vector2(0, 20);
        }

        public void HandleButtonRelease()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.Shop);
            buttonText.rectTransform.anchoredPosition += new Vector2(0, 20);
        }
    }
}
