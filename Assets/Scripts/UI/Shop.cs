using UnityEngine;

namespace UI
{
    /// <summary>
    /// Object that is used for purchasing upgrades
    /// </summary>
    public class Shop : MyCanvas
    {
        [SerializeField] private CoinFarmer farmer;
        public override CanvasLayer CanvasLayer => CanvasLayer.Shop;

        public void HandlePurchase(UpgradeMessage message)
        {
            farmer.PointsPerClick += message.ClickBonus;
            farmer.PointsPerSecond += message.AutoClickBonus;
        }

        public void CloseShop()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.PlayerHUD);
        }
    }
}