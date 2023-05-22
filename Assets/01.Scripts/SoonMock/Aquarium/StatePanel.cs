using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class StatePanel : MonoBehaviour
{
    public GameObject upgradeObj;
    public int cost;
    [SerializeField]public int selectedDeco
    {
        get; 
        set;
    }
    [SerializeField]public int selectedFish
    {
        get;
        set;
    }
    [SerializeField] public int selectedAmount
    {
        get;
        set;
    }
    Facility _script;
    private void OnEnable()
    {
        _script = upgradeObj.GetComponent<Facility>();

    }
    public void UpgradeLevel()
    {
        //����� �ִ뷮 �߰�
        if(_script.GetComponent<Fishbowl>())
        {
            _script.GetComponent<Fishbowl>().Upgrade();
        }else if(_script.GetComponent<SnackShop>())
        {
            _script.GetComponent<SnackShop>().Upgrade();
        }
    }
    public void AddFish()
    {
        //����� �߰�
        if (_script.GetComponent<Fishbowl>())
        {
            Debug.Log("asdf");
            _script.GetComponent<Fishbowl>().AddFish(selectedFish,selectedAmount);
        }

    }
    public void AddDeco()
    {
        if (_script.GetComponent<Fishbowl>())
        {
            _script.GetComponent<Fishbowl>().AddDeco(selectedDeco);
        }
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    public void ChangePos()
    {
        gameObject.SetActive(false);
        //AquariumManager.Instance.ChangeFacilityPos(upgradeObj.GetComponent<Facility>());
    }
}
