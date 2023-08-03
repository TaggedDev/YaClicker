using System.Numerics;
using TMPro;
using UnityEngine;

/// <summary>
/// Instance of coin source (one of three types)
/// </summary>
public class CoinFarmer : MonoBehaviour
{
    [SerializeField] private int maxBuildingTier;
    [SerializeField] private TextMeshProUGUI balanceText;
    // [sf] private shop shop;
        
    private int _buildingTier = 0;
    private double _pointsBalance = 0;

    private double PointsBalance
    {
        get => _pointsBalance;
        set
        {
            balanceText.text = _pointsBalance.ToString();
            _pointsBalance = value;
        }
    }

    public double PointsPerClick { get; set; } = 1;
    public double PointsPerSecond { get; set; } = 1;

    private float _passiveIncomeTick = 1;
        
    private void Update()
    {
        _passiveIncomeTick -= Time.deltaTime;
        if (_passiveIncomeTick > 0)
            return;

        _passiveIncomeTick = 1;
        HandlePassiveIncome();
    }

    /// <summary>
    /// Adds passive points to balance
    /// </summary>
    private void HandlePassiveIncome()
    {
        PointsBalance += PointsPerSecond;
    }

    private void OnMouseDown()
    {
        HandleObjectClick();
    }

    /// <summary>
    /// Adds points for one click
    /// </summary>
    public void HandleObjectClick() => PointsBalance += PointsPerClick;
        
    /// <summary>
    /// Increases building level
    /// </summary>
    public void UpgradeBuilding()
    {
        if (_buildingTier == maxBuildingTier)
            return;
        
        _buildingTier++;
        PointsPerClick++;
    }
}