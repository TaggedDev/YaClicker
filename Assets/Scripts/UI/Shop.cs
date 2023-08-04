using Economy;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Object that is used for purchasing upgrades
    /// </summary>
    public class Shop : ClickerCanvas
    {
        [SerializeField] private Cell shopCellPrefab;
        [SerializeField] private GridLayoutGroup gridParent;
        public override CanvasLayer CanvasLayer => CanvasLayer.Shop;
        private CoinFarmer _farmer;

        public void AttachFarmerToShop(CoinFarmer farmer)
        {
            _farmer = farmer;
        }
        
        public void CloseShop()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.PlayerHUD);
        }

        public void LoadUpgrades()
        {
            var items = Resources.LoadAll<UpgradeMessage>("Upgrades");
            foreach (var message in items)
            {
                var cell = Instantiate(shopCellPrefab, gridParent.transform, true);
                cell.AttachUpgradeToCell(message, _farmer);
            }
        }
    }
}