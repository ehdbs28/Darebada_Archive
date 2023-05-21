using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIScreen : MonoBehaviour
{
    [SerializeField]
    protected VisualTreeAsset _treeAsset;

    public virtual void SetUp(UIDocument document, bool clearScreen = true){
        VisualElement root = document.rootVisualElement;

        if(clearScreen)
            root.Clear();

        VisualElement generatedRoot = GenerateRoot();

        if(generatedRoot != null){
            AddEvent(generatedRoot);
            root.Add(generatedRoot);
        }
    }

    protected abstract VisualElement GenerateRoot();
    protected abstract void AddEvent(VisualElement root);
}
