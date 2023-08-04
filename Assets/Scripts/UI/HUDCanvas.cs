namespace UI
{
    public class HUDCanvas : MyCanvas
    {
        public override CanvasLayer CanvasLayer => CanvasLayer.PlayerHUD;

        public void OpenShopCanvas()
        {
            CanvasLayersController.EnableCanvasOfLayer(CanvasLayer.Shop);
        }
    }
}
