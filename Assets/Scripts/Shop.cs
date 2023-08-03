using UnityEngine;

/// <summary>
/// Object that is used for purchasing upgrades
/// </summary>
public class Shop : MonoBehaviour
{
    [SerializeField] private CoinFarmer farmer;

    public void HandlePurchase(UpgradeMessage message)
    {
        farmer.PointsPerClick += message.ClickBonus;
        farmer.PointsPerSecond += message.AutoClickBonus;
    }
}