using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] GameObject fixUIpanel;
    GameObject alreadyOpenUI;

    public void OnUpgradeOpen(GameObject openUI)
    {
        if (alreadyOpenUI != null)
        {
            alreadyOpenUI.gameObject.SetActive(false);
        }

        openUI.SetActive(true);
        alreadyOpenUI = openUI;
    }
}
