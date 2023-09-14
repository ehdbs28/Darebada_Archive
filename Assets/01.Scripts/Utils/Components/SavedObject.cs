using UnityEngine;

public class SavedObject : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] private GameObject _initObj;

    private GameObject _savedObj;
    
    public void Save()
    {
        ES3.Save<GameObject>(_key, _savedObj);
        Destroy(_savedObj);
        _savedObj = null;
    }

    public void Load()
    {
        if (ES3.KeyExists(_key))
            _savedObj = ES3.Load<GameObject>(_key);
        else
            _savedObj = Instantiate(_initObj, transform, true);

        _savedObj.transform.name = _savedObj.transform.name.Replace("(Clone)", "");
    }
}
