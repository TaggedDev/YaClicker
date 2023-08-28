using System;
using System.Collections.Generic;
using Economy;
using UI;
using UnityEngine;

public class SaveLoader : MonoBehaviour
{
    [Tooltip("First canvas must be player HUD")] [SerializeField]
    private List<ClickerCanvas> canvases;
    [SerializeField] private PlayerResource coinAmount;
    [SerializeField] private PlayerResource uraniumAmount;
    [SerializeField] private CoinFarmer farmer;

    /// <summary>
    /// Coins balance
    /// </summary>
    public PlayerResource CoinAmount => coinAmount;
    
    /// <summary>
    /// Uranium balance
    /// </summary>
    public PlayerResource UraniumAmount => uraniumAmount;


    private void Start()
    {
        if (coinAmount is null || uraniumAmount is null)
            throw new ArgumentException("Resources are not set in SaveLoader");
        
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