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
        _documentRoot = document.rootVisualElement;

        if(clearScreen)
            _documentRoot.Clear();

        VisualElement generatedRoot = GenerateRoot();

        if(generatedRoot != null){
            AddEvent(generatedRoot);
            _documentRoot.Add(generatedRoot);
        }
    }

    protected virtual VisualElement GenerateRoot(){
        _root = _treeAsset.Instantiate();
        _root = _root.Q<VisualElement>("container");
        
        FindElement(_root);

        return _root;
    }

    protected abstract void AddEvent(VisualElement root);
    protected abstract void FindElement(VisualElement root);
}
