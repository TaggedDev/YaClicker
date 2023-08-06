using TMPro;
using UnityEngine;

namespace UI
{
    public class MainMenu : ClickerCanvas
    {
        public override CanvasLayer CanvasLayerTag => CanvasLayer.MainMenu;
        [SerializeField] private TextMeshProUGUI balanceText;

        public void OpenShopCanvas()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.Shop);
        }

        public void UpdateBalance(double newBalance, double pointsPerClick, double pointsPerAutoClick)
        {
            balanceText.text = $"{newBalance:N3} [${pointsPerClick}, ${pointsPerAutoClick}]";
        }
    }
}
