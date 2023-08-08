using Economy;
using UnityEngine;
using UnityEngine.AI;
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
        [SerializeField] private SaveLoader loader;
        
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
                cell.AttachUpgradeToCell(message, loader);
            }

            // Trigger 'not enough money' event on buttons on game loads
            foreach (var resource in loader.Resources)
                resource.ResourceBank = 0;
            
            gameObject.SetActive(false);
        }
    }
}