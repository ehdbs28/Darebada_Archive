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
        VisualElement root = _treeAsset.Instantiate();
        root = root.ElementAt(0);
        
        FindElement(root);

        return root;
    }

    protected override abstract void AddEvent(VisualElement root);
    protected override abstract void FindElement(VisualElement root);
}
