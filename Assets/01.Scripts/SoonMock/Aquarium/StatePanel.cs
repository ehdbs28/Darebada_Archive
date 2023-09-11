using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
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

    }
    public FishDataUnit tempUnit;
    public void Update()
    {

    }
    public void UpgradeLevel()
    {
        //물고기 최대량 추가
        _script = upgradeObj .GetComponent<Facility>();
        if(_script.GetComponent<Fishbowl>())
        {

        }
        else if(_script.GetComponent<SnackShop>())
        {
            _script.GetComponent<SnackShop>().Upgrade();
        }
    }
    public void AddFish(FishDataUnit dataUnit)
    {
        //물고기 추가
        if (_script.GetComponent<Fishbowl>())
        {
            Debug.Log("asdf");
            _script.GetComponent<Fishbowl>().AddFIsh(dataUnit);
        }

    }
    public void AddDeco()
    {
        if (_script.GetComponent<Fishbowl>())
        {
            //_script.GetComponent<Fishbowl>().AddDeco(selectedDeco);
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
