using System.Linq;
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
        [SerializeField] private VerticalLayoutGroup layoutParent;
        [SerializeField] private SaveLoader loader;
        
        public override CanvasLayer CanvasLayerTag => CanvasLayer.Shop;

        public void CloseShop()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.MainMenu);
        }

        private void Start()
        {
            var items = Resources.LoadAll<UpgradeMessage>("Upgrades").OrderBy(x => x.UpgradeID);
            Cell previousCell = null;
            foreach (var message in items)
            {
                var cell = Instantiate(shopCellPrefab, layoutParent.transform, true);
                cell.AttachUpgradeToCell(message, loader, previousCell);
                previousCell = cell;
            }

            // Trigger 'not enough money' event on buttons on game loads
            foreach (var resource in loader.Resources)
                resource.ResourceBank = 0;
            
            gameObject.SetActive(false);
        }
    }
}