using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeFixUI : MonoBehaviour
{
    [SerializeField] GameObject fixUIpanel;
    [SerializeField] GameObject upgradeDefaultUI;
    [SerializeField] GameObject fixDefaultUI;

    [Header("Ship Upgrade Button")]
    [SerializeField] Button shipSpeedUpgradeBtn;
    [SerializeField] Button shipInventoryUpgradeBtn;
    [SerializeField] Button shipDurablityUpgradeBtn;
    [Header("Ship fix Button")]
    [SerializeField] Button shipFixBtn;
    [Header("FishingRod Upgrade Button")]
    [SerializeField] Button fishingRodPowerUpgradeBtn;
    [SerializeField] Button fishingRodLengthUpgradeBtn;
    [Header("FishingRod fix Button")]
    [SerializeField] Button fishingRodFixBtn;
    
    GameObject alreadyOpenUI;

    bool isStateUpgrade = true;

    private void Start()
    {
        alreadyOpenUI = upgradeDefaultUI;
    }

    public void OnUpgradeUI() {
        alreadyOpenUI?.SetActive(false);
        
        isStateUpgrade = true;
        upgradeDefaultUI.SetActive(true);

        alreadyOpenUI = upgradeDefaultUI;
    }
    public void OnFixUI()
    {
        alreadyOpenUI?.SetActive(false);

        isStateUpgrade = false;
        fixDefaultUI.SetActive(true);

        alreadyOpenUI = fixDefaultUI;
    }

    public void OnUpgradeOpen(GameObject openUI)
    {
        if (isStateUpgrade)
        {
            alreadyOpenUI?.gameObject.SetActive(false);

            openUI.SetActive(true);
            alreadyOpenUI = openUI;
        }
    }
    public void OnFixOpen(GameObject openUI)
    {
        if (!isStateUpgrade)
        {
            alreadyOpenUI?.gameObject.SetActive(false);

            openUI.SetActive(true);
            alreadyOpenUI = openUI;
        }
    }


}
