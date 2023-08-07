using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    [Flags]
    public enum CanvasLayer
    {
        None = 1 << 0,
        MainMenu = 1 << 1,
        Shop = 1 << 2
    }


    public static class CanvasLayersController
    {
        public static List<ClickerCanvas> Canvases { get; } = new();

        public static void EnableCanvasOfLayer(CanvasLayer affectedLayer)
        {
            foreach (var canvas in Canvases)
                canvas.gameObject.SetActive(false);
            
            foreach (var canvas in Canvases)
            {
                if (!canvas.CanvasLayerTag.HasFlag(affectedLayer)) continue;
                
                canvas.gameObject.SetActive(true);
                return;
            }
        }
    }
}