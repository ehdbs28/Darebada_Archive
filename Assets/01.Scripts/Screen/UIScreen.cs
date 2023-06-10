using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIScreen : MonoBehaviour
{
    [SerializeField]
    protected VisualTreeAsset _treeAsset;

    protected VisualElement _documentRoot = null;
    protected VisualElement _root = null;

    public virtual void SetUp(UIDocument document, bool clearScreen = true){
        _documentRoot = document.rootVisualElement.Q("main-container");

        if(clearScreen && _documentRoot.childCount >= 2){
            for(int i = 0; i < _documentRoot.childCount; i++){
                if(_documentRoot.ElementAt(i).ClassListContains("blur-panel")) continue;
                _documentRoot.RemoveAt(i);
            }
        }

        VisualElement generatedRoot = GenerateRoot();

        if(generatedRoot != null){
            AddEvent(generatedRoot);
            _documentRoot.Insert(0, generatedRoot);
        }
    }

    protected virtual VisualElement GenerateRoot(){
        _root = _treeAsset.Instantiate();
        _root = _root.Q<VisualElement>("container");
        
        FindElement(_root);

        return _root;
    }

    protected abstract void AddEvent(VisualElement root);
    public abstract void RemoveEvent();
    protected abstract void FindElement(VisualElement root);
}
