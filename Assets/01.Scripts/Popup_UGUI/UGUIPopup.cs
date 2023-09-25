using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UGUIPopup : MonoBehaviour, IUI
{
    [SerializeField]
    protected GameObject _uiObject;

    protected VisualElement _documentRoot = null;

    protected bool _isOpenPopup = false;
    public bool IsOpenPopup => _isOpenPopup;

    protected VisualElement _blurPanel = null;

    public virtual void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        _isOpenPopup = true;

        _documentRoot = document.rootVisualElement.Q("main-container");

        if(clearScreen && _documentRoot.childCount >= 3){
            for(int i = 0; i < _documentRoot.childCount; i++){
                if(_documentRoot.ElementAt(i).ClassListContains("blur-panel") || _documentRoot.ElementAt(i).ClassListContains("notification")) 
                    continue;
                _documentRoot.RemoveAt(i);
            }
        }

        if(timeStop)
            GameManager.Instance.GetManager<TimeManager>().TimeScale = 0f;

        if(blur){
            _blurPanel = _documentRoot.Q(className: "blur-panel");
            _blurPanel.AddToClassList("on");
        }

        GenerateRoot();
        AddEvent();
    }

    public virtual void GenerateRoot()
    {
        _uiObject.SetActive(true);
        FindElement();
    }


    public virtual void RemoveRoot()
    {
        _uiObject.SetActive(false);
        RemoveEvent();
        _isOpenPopup = false;
    }

    public abstract void AddEvent();
    public abstract void FindElement();
    public abstract void RemoveEvent();
}
