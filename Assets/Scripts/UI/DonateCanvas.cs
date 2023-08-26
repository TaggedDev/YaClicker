using System;

namespace UI
{
    public class DonateCanvas : ClickerCanvas
    {
        public override CanvasLayer CanvasLayerTag => CanvasLayer.UraniumDonate;

        private void Start()
        {
            CanvasLayersController.Canvases.Add(this);
            gameObject.SetActive(false);
        }

        public void CloseShop()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.MainMenu);
        }
    }
}