using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIPopup : UIScreen
{
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
            AddEvent(_root);
            _documentRoot.Add(_root);
        }
    }

    protected override VisualElement GenerateRoot()
    {
        _root = _treeAsset.Instantiate();
        _root = _root.ElementAt(0);
        
        FindElement(_root);

        return _root;
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
}
