using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UICompass
{
    private VisualElement _directionRoot;
    private List<Label> _directioUnits;

    public UICompass(VisualElement compassRoot){
        _directionRoot = compassRoot.Q<VisualElement>("direction");
        _directioUnits = _directionRoot.Query<Label>().ToList();
    }

    public void SetRotate(float rotate){
        _directionRoot.style.rotate = new StyleRotate(new Rotate(rotate));
        foreach(var directionUnit in _directioUnits){
            directionUnit.style.rotate = new StyleRotate(new Rotate(-rotate));
        }
    }
}
