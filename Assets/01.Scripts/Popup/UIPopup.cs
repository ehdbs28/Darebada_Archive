using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIPopup : MonoBehaviour, IUI
{
    [SerializeField]
    protected VisualTreeAsset _treeAsset;

    protected VisualElement _documentRoot = null;
    protected VisualElement _root = null;

    protected bool _isOpenPopup = false;
    public bool IsOpenPopup => _isOpenPopup;

    protected VisualElement _blurPanel = null;

    public virtual void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true){
        _isOpenPopup = true;

        _documentRoot = document.rootVisualElement.Q("main-container");

        if(clearScreen && _documentRoot.childCount >= 2){
            for(int i = 0; i < _documentRoot.childCount; i++){
                if(_documentRoot.ElementAt(i).ClassListContains("blur-panel")) continue;
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

        if(_root != null){
            AddEvent();
            _documentRoot.Add(_root);
        }
    }

    public virtual void GenerateRoot()
    {
        _root = _treeAsset.Instantiate();
        _root = _root.ElementAt(0);
        
        FindElement();
    }

    public virtual void RemoveRoot(){
        if(_documentRoot == null || _root == null){
            return;
        }

        RemoveEvent();

        _documentRoot.Remove(_root);

        if(_blurPanel != null)
            _blurPanel.RemoveFromClassList("on");

        _documentRoot = null;
        _root = null;
        _blurPanel = null;

        GameManager.Instance.GetManager<TimeManager>().TimeScale = 1f;

        _isOpenPopup = false;
    }

    public abstract void AddEvent();
    public abstract void RemoveEvent();
    public abstract void FindElement();
}
