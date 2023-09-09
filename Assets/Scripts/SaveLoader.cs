using System;
using System.Collections.Generic;
using Economy;
using UI;
using UnityEngine;
using YG;

public class SaveLoader : MonoBehaviour
{
    
    [Tooltip("First canvas must be player HUD")] [SerializeField]
    private List<ClickerCanvas> canvases;
    [SerializeField] private PlayerResource coinAmount;
    [SerializeField] private PlayerResource uraniumAmount;
    [SerializeField] private Shop shop;
    [SerializeField] private CoinFarmer farmer;

    /// <summary>
    /// Coins balance
    /// </summary>
    public PlayerResource CoinAmount => coinAmount;
    
    /// <summary>
    /// Uranium balance
    /// </summary>
    public PlayerResource UraniumAmount => uraniumAmount;

    /// <summary>
    /// Applies boost on CoinFarmer with given params
    /// </summary>
    /// <param name="multiplier">Boost coefficient</param>
    /// <param name="secondsDuration">Boost duration</param>
    public void ApplyBoost(float multiplier, float secondsDuration)
    {
        farmer.HandleIncomeBoost(multiplier, secondsDuration);
    }

    private void OnEnable() =>YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;
    
    private void GetLoad()
    {
        coinAmount.ResourceBank = YandexGame.savesData.PlayerCoins;
        uraniumAmount.ResourceBank = YandexGame.savesData.PlayerUranium;

        for (int i = 0; i < shop.Cells.Length; i++)
            shop.Cells[i].UpgradeLevel = YandexGame.savesData.PlayerUpgradesLevels[i];

        coinAmount.ResourcePerClick = YandexGame.savesData.PlayerCoinPerClick;
        coinAmount.ResourcePerAutoClick = YandexGame.savesData.PlayerCoinPerAutoClick;
    }

    
    public void SaveProgress()
    {
        YandexGame.savesData.PlayerCoins = coinAmount.ResourceBank;
        YandexGame.savesData.PlayerUranium = uraniumAmount.ResourceBank;
        for (int i = 0; i < shop.Cells.Length; i++)
            YandexGame.savesData.PlayerUpgradesLevels[i] = shop.Cells[i].UpgradeLevel;
        YandexGame.savesData.PlayerCoinPerClick = coinAmount.ResourcePerClick;
        YandexGame.savesData.PlayerCoinPerAutoClick = coinAmount.ResourcePerAutoClick;
        
        YandexGame.SaveProgress();
    }

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
}
