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

    private static PlayerResource _staticCoin;
    private static PlayerResource _staticUranium;
    private static Shop _staticShop;

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
        {
            shop.Cells[i].UpgradeLevel = YandexGame.savesData.PlayerUpgradesLevels[i];
            var upgrade = shop.Cells[i];
            coinAmount.ResourcePerClick += upgrade.GetCurrentClickBonus();
            coinAmount.ResourcePerAutoClick += upgrade.GetCurrentAutoClickBonus();
            upgrade.SelectBorder();
        }

        if (coinAmount.ResourcePerClick == 0.0)
            coinAmount.ResourcePerClick = 0.3;
        if (coinAmount.ResourcePerAutoClick == 0.0)
            coinAmount.ResourcePerClick = 0.56;
    }

    public static void SaveProgress()
    {
        YandexGame.savesData.PlayerCoins = _staticCoin.ResourceBank;
        YandexGame.savesData.PlayerUranium = _staticUranium.ResourceBank;
        for (int i = 0; i < _staticShop.Cells.Length; i++)
            YandexGame.savesData.PlayerUpgradesLevels[i] = _staticShop.Cells[i].UpgradeLevel;
        
        YandexGame.SaveProgress();
    }

    private void Start()
    {
        if (coinAmount is null || uraniumAmount is null)
            throw new ArgumentException("Resources are not set in SaveLoader");

        _staticCoin = coinAmount;
        _staticUranium = uraniumAmount;
        _staticShop = shop;
        
        HandleCanvasLoading();
    }
    
    private void HandleCanvasLoading()
    {
        CanvasLayersController.Farmer = farmer;
        CanvasLayersController.Canvases.AddRange(canvases);
    }
}
