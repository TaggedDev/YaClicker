using System.Numerics;
using UnityEngine;

/// <summary>
/// Changes that provide bought upgrade
/// </summary>
[CreateAssetMenu(fileName = "DefaultUpgrade", menuName = "Upgrade", order = 1)]
public class UpgradeMessage : ScriptableObject
{
    public double _clickBonus;
    public double _autoClickBonus;

    public double ClickBonus => _clickBonus;
    public double AutoClickBonus => _autoClickBonus;
}