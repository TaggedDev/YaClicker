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
    [SerializeField] private PlayerProfilePicture playerPicture;
    [SerializeField] private DonateShop donateShop;
    
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

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
        YandexGame.RewardVideoEvent += HandleRewardAdWatch;

    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
        YandexGame.RewardVideoEvent -= HandleRewardAdWatch;
    } 

    private void Start()
    {
        if (coinAmount is null || uraniumAmount is null)
            throw new ArgumentException("Resources are not set in SaveLoader");

        HandleCanvasLoading();
    }

    private void GetLoad()
    {
        LoadData();
    }

    private void HandleCanvasLoading()
    {
        CanvasLayersController.Farmer = farmer;
        CanvasLayersController.Canvases.AddRange(canvases);
    }

    private void HandleRewardAdWatch(int id)
    {
        donateShop.GivePlayerReward(id);
    }
    
    public void SaveData()
    {
        YandexGame.savesData.PlayerResourceValues[0] = coinAmount.ResourceBank;
        YandexGame.savesData.PlayerResourceValues[1] = coinAmount.ResourceBank;
        for (int i = 0; i < shop.Cells.Length; i++)
            YandexGame.savesData.PlayerUpgradesLevels[i] = shop.Cells[i].UpgradeLevel;
        
        
        // DEBUG
        SaveDataEditorly();
        /*if (YandexGame.auth)
            SaveDataCloudly();
        else
            SaveDataLocally();*/
    }

    public void AuthSuccess()
    {
        playerPicture.SetPlayerPicture();
        LoadData();
        //LoadCloudData();
    }
    
    private void LoadData()
    {
        coinAmount.ResourceBank = YandexGame.savesData.PlayerResourceValues[0];
        uraniumAmount.ResourceBank = YandexGame.savesData.PlayerResourceValues[1];
        for (int i = 0; i < shop.Cells.Length; i++)
            shop.Cells[i].UpgradeLevel = YandexGame.savesData.PlayerUpgradesLevels[i]; 
        
        Debug.Log("Loaded information");
    }

    private void SaveDataEditorly()
    {
        YandexGame.SaveEditor();
    }
    
    private void SaveDataLocally()
    {
        YandexGame.SaveLocal();
    }

    private void SaveDataCloudly()
    {
        YandexGame.SaveCloud();
    }
}
