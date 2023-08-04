using System;
using System.Collections.Generic;
using System.Linq;

namespace UI
{
    [Flags]
    public enum CanvasLayer
    {
        None,
        PlayerHUD,
        Shop,
        All = PlayerHUD | Shop
    }


    public static class CanvasLayersController
    {
        public static List<ClickerCanvas> Canvases { get; } = new();
        public static ClickerCanvas OpenedCanvas { get; set; }

        public static void EnableCanvasOfLayer(CanvasLayer affectedLayer)
        {
            OpenedCanvas.gameObject.SetActive(false);

            foreach (var canvas in Canvases.Where(canvas => canvas.CanvasLayer.HasFlag(affectedLayer)))
            {
                OpenedCanvas = canvas;
                canvas.gameObject.SetActive(true);
                return;
            }
            
            
        }
    }
}