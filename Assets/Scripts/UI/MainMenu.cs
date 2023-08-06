using TMPro;
using UnityEngine;

namespace UI
{
    public class MainMenu : ClickerCanvas
    {
        public override CanvasLayer CanvasLayer => CanvasLayer.MainMenu;
        [SerializeField] private TextMeshProUGUI balanceText;

        public void OpenShopCanvas()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.Shop);
        }

        public void UpdateBalance(double newBalance)
        {
            balanceText.text = $"{newBalance:N3}";
        }
    }
}
