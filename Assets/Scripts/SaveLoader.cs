using System;
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

    /// <summary>
    /// Coin, uranium, power, iron, cobalt, gold
    /// </summary>
    public PlayerResource[] Resources => resources;


    private void Start()
    {
        if (resources.Length != CoinFarmer.ResourcesAmount)
            throw new ArgumentException("Resources are not set in SaveLoader object or their length not equal ResourceAmount");
        
        HandleCanvasLoading();
        
    }

    private void HandleCanvasLoading()
    {
        CanvasLayersController.Farmer = farmer;
        CanvasLayersController.Canvases.AddRange(canvases);
    }

    /// <summary>
    /// Applies boost on CoinFarmer with given params
    /// </summary>
    /// <param name="multiplier">Boost coefficient</param>
    /// <param name="secondsDuration">Boost duration</param>
    public void ApplyBoost(float multiplier, float secondsDuration)
    {
        farmer.HandleIncomeBoost(multiplier, secondsDuration);
    }
}