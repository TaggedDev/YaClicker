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
        [SerializeField] private CoinFarmer farmer;
        [SerializeField] private PlayerResource[] playerResource;
        
        public override CanvasLayer CanvasLayerTag => CanvasLayer.Shop;

        public void CloseShop()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.MainMenu);
        }

        private void Start()
        {
            var items = Resources.LoadAll<UpgradeMessage>("Upgrades");
            foreach (var message in items)
            {
                var cell = Instantiate(shopCellPrefab, gridParent.transform, true);
                cell.AttachUpgradeToCell(message, farmer);
            }
        }
    }
}