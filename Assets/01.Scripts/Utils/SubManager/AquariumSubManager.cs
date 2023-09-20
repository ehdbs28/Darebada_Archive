using System;
using Unity.VisualScripting;
using UnityEngine;

public class AquariumSubManager : MonoBehaviour
{
    private GameObject _aquariumObj = null;

    public void OnEnterScene()
    {
        _aquariumObj = ((AquariumSaveData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.AquariumSaveData))
            .LoadObj();

        _aquariumObj = Instantiate(_aquariumObj, transform, true);

        _aquariumObj.transform.name = _aquariumObj.transform.name.Replace("(Clone)", "");
        
        _aquariumObj.GetComponent<AquariumManager>().InitManager();
        _aquariumObj.GetComponent<AquariumManager>().GenerateCustomer();
    }
    
    public void OnExitScene()
    {
        _aquariumObj.GetComponent<AquariumManager>().ReleaseManager();
        ((AquariumSaveData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.AquariumSaveData))
            .SaveObj(_aquariumObj);
        GameManager.Instance.GetManager<DataManager>().GetData(DataType.AquariumData).Save();
        Destroy(_aquariumObj.gameObject);
    }
}
