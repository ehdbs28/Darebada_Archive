using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIPopup : UIScreen
{
    private bool _isOpenPopup = false;
    public bool IsOpenPopup => _isOpenPopup;

    VisualElement _blurPanel = null;

    public void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true){
        _isOpenPopup = true;

        _documentRoot = document.rootVisualElement.Q("main-container");

        if(clearScreen && _documentRoot.childCount >= 2)
            _documentRoot.RemoveAt(0);

        if(timeStop)
            GameManager.Instance.GetManager<TimeManager>().TimeScale = 0f;

        if(blur){
            _blurPanel = _documentRoot.Q(className: "blur-panel");
            _blurPanel.AddToClassList("on");
        }

        VisualElement generatedRoot = GenerateRoot();

        if(generatedRoot != null){
            AddEvent(generatedRoot);
            _documentRoot.Add(generatedRoot);
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
        if(_documentRoot == null || _root == null || _blurPanel == null){
            return;
        }

        _documentRoot.Remove(_root);
        _blurPanel.RemoveFromClassList("on");

        _documentRoot = null;
        _root = null;
        _blurPanel = null;

        GameManager.Instance.GetManager<TimeManager>().TimeScale = 1f;

        _isOpenPopup = false;
    }

    protected override abstract void AddEvent(VisualElement root);
    protected override abstract void FindElement(VisualElement root);
}
