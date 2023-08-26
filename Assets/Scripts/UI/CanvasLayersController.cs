using System;
using System.Collections.Generic;
using System.Linq;
using Economy;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    [Flags]
    public enum CanvasLayer
    {
        None = 1 << 0,
        MainMenu = 1 << 1,
        Shop = 1 << 2,
        UraniumDonate = 1 << 3,
        UraniumShop = 1 << 4
    }


    public static class CanvasLayersController
    {
        public static CoinFarmer Farmer;
        public static List<ClickerCanvas> Canvases { get; } = new();

        public static void EnableCanvasOfLayer(CanvasLayer affectedLayer)
        {
            Farmer.SetActive(affectedLayer == CanvasLayer.MainMenu);

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