using System.Collections.Generic;
using Economy;
using UI;
using UnityEngine;

public class SaveLoader : MonoBehaviour
{
    [Tooltip("First canvas must be player HUD")] [SerializeField]
    private List<ClickerCanvas> canvases;
    [Tooltip("Coins, Uranium, Power, Iron, Cobalt, Gold")] [SerializeField]
    private PlayerResource[] resources;

    [SerializeField] private CoinFarmer farmer;

    public PlayerResource[] Resources => resources;


    private void Start()
    {
        HandleCanvasLoading();
    }

    private void HandleCanvasLoading()
    {
        CanvasLayersController.Farmer = farmer;
        CanvasLayersController.Canvases.AddRange(canvases);
    }
}