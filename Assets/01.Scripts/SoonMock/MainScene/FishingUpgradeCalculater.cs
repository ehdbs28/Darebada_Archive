using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingUpgradeCalculater : MonoBehaviour
{
    public int CalcShipSpeedCost(int upgradeAmount)
    {
        return upgradeAmount * upgradeAmount * 200;
    }
    public int CalcShipStorageCost(int upgradeAmount)
    {
        return upgradeAmount * upgradeAmount * 500;
    }
    public int CalcShipDurability(int upgradeAmount)
    {
        return upgradeAmount * upgradeAmount * 100;
    }
    public int CalcFishingStickWeight(int upgradeAmount)
    {
        return upgradeAmount * upgradeAmount * 150;
    }
    public int CalcFishingStickLength(int upgradeAmount)
    {
        return upgradeAmount * upgradeAmount * 210;
    }
    public int CalcFishingStickStrength(int upgradeAmount)
    {
        return upgradeAmount * upgradeAmount * 270;
    }
}
