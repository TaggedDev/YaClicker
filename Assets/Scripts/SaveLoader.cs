using System;
using System.Collections.Generic;
using Economy;
using UI;
using UnityEngine;

public class SaveLoader : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private CoinFarmer coinFarmer;
    [SerializeField] private Shop shop;
    [Tooltip("First canvas must be player HUD")] [SerializeField] private List<ClickerCanvas> canvases;

    private void Start()
    {
        HandleShopLoading();
        HandleCanvasLoading();
    }

    private void HandleShopLoading()
    {
        var shopEntity = Instantiate(shop, mainCanvas.transform);
        canvases.Add(shopEntity);
        shopEntity.gameObject.SetActive(false);
        shopEntity.Farmer = coinFarmer;
        shopEntity.LoadUpgrades();
    }

    private void HandleCanvasLoading()
    {
        CanvasLayersController.Canvases.AddRange(canvases);
        CanvasLayersController.OpenedCanvas = canvases[0];

        if (CanvasLayersController.OpenedCanvas.CanvasLayer != CanvasLayer.PlayerHUD)
            throw new ArgumentException("Opened canvas doesn't have PlayerHUD layer. First canvas must be HUD");
    }
}