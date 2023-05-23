using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIPopup : UIScreen
{
    public override void SetUp(UIDocument document, bool clearScreen = false)
    {
        base.SetUp(document, clearScreen);
    }

    protected override VisualElement GenerateRoot()
    {
        _root = _treeAsset.Instantiate();
        _root = _root.ElementAt(0);
        
        FindElement(_root);

        return _root;
    }

    protected virtual void RemoveRoot(){
        if(_documentRoot == null || _root == null){
            return;
        }

        _documentRoot.Remove(_root);
    }


    protected override abstract void AddEvent(VisualElement root);
    protected override abstract void FindElement(VisualElement root);
}
