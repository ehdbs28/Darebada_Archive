using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIScreen : MonoBehaviour, IUI
{
    [SerializeField]
    protected VisualTreeAsset _treeAsset;

    protected VisualElement _documentRoot = null;
    protected VisualElement _root = null;

    public virtual void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true){
        _documentRoot = document.rootVisualElement.Q("main-container");

        if(clearScreen && _documentRoot.childCount >= 3){
            for(int i = 0; i < _documentRoot.childCount; i++){
                if(_documentRoot.ElementAt(i).ClassListContains("blur-panel") || _documentRoot.ElementAt(i).ClassListContains("notification")) 
                    continue;
                
                _documentRoot.RemoveAt(i);
            }
        }

        GenerateRoot();

        if(_root != null){
            AddEvent();
            _documentRoot.Insert(0, _root);
        }
    }

    public virtual void GenerateRoot(){
        _root = _treeAsset.Instantiate();
        _root = _root.Q<VisualElement>("container");
        
        FindElement();
    }

    public void RemoveRoot()
    {
        RemoveEvent();
    }

    public abstract void AddEvent();
    public abstract void RemoveEvent();
    public abstract void FindElement();

}
