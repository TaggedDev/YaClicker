using TMPro;
using UnityEngine;

namespace UI
{
    public class HUDCanvas : ClickerCanvas
    {
        public override CanvasLayer CanvasLayer => CanvasLayer.PlayerHUD;
        [SerializeField] private TextMeshProUGUI balanceText;

        public void OpenShopCanvas()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.Shop);
        }

        public void UpdateBalance(double newBalance)
        {
            balanceText.text = newBalance.ToString();
        }
    }
}
