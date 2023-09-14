using UnityEngine;

public class SavedObject : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] private GameObject _initObj;
    [SerializeField] private Material _floorMat;

    private GameObject _savedObj;
    
    public void Save()
    {
        _savedObj.GetComponent<AquariumManager>().ReleaseManager();
        
        ES3.Save<GameObject>(_key, _savedObj);
        Destroy(_savedObj);
        _savedObj = null;
    }

    public void Load()
    {
        if (ES3.KeyExists(_key))
            _savedObj = ES3.Load<GameObject>(_key);
        else
            _savedObj = Instantiate(_initObj);

        _savedObj.transform.Find("Floor").GetComponent<MeshRenderer>().material = _floorMat;
        _savedObj.transform.SetParent(transform);
        _savedObj.transform.name = _savedObj.transform.name.Replace("(Clone)", "");
        
        _savedObj.GetComponent<AquariumManager>().InitManager();
        _savedObj.GetComponent<AquariumManager>().GenerateCustomer();
    }
}
