using System.Linq;
using Economy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Object that is used for purchasing upgrades
    /// </summary>
    public class Shop : ClickerCanvas, IPressableButton
    {
        [SerializeField] private Cell shopCellPrefab;
        [SerializeField] private VerticalLayoutGroup layoutParent;
        [SerializeField] private SaveLoader loader;
        [SerializeField] private TextMeshProUGUI buttonText;

        private Cell[] _cells = new Cell[20];
        public Cell[] Cells => _cells;

        public override CanvasLayer CanvasLayerTag => CanvasLayer.Shop;
        
        private void Start()
        {
            var items = Resources.LoadAll<UpgradeMessage>("Upgrades").OrderBy(x => x.UpgradeID);
            Cell previousCell = null;
            byte i = 0;
            foreach (var message in items)
            {
                var cell = Instantiate(shopCellPrefab, layoutParent.transform, true);
                cell.RectTransform.localScale = Vector3.one;
                cell.AttachUpgradeToCell(message, loader, previousCell);
                previousCell = cell;
                
                _cells[i] = cell;
                i++;
            }

            // Trigger 'not enough money' event on buttons on game loads
            loader.CoinAmount.ResourceBank = 0;
            loader.UraniumAmount.ResourceBank = 0;

            gameObject.SetActive(false);
        }

        public void HandleButtonPress()
        {
            buttonText.rectTransform.anchoredPosition -= new Vector2(0, 20);
        }

        public void HandleButtonRelease()
        {
            buttonText.rectTransform.anchoredPosition += new Vector2(0, 20);
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.MainMenu);
        }

        public void HandleUraniumShopOpen()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.UraniumShop);
        }
    }
}