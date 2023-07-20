using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DictionaryPopup : UIPopup
{
    private VisualElement _exitBtn;
    private VisualElement _container;

    private DictionaryDetail _detailView;

    private List<DictionaryUnit> _fishes = new List<DictionaryUnit>();

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });

        foreach(var unit in _fishes){
            unit.Root.RegisterCallback<ClickEvent>(e => {
                if(unit.IsUnknown)
                    return;
                SetDetail(unit.DataUnit);
            });
        }
    }

    private void SetDetail(DictionaryDataUnit data){
        _detailView.SetUp(data);
        _detailView.Show(true);
    }
    
    public override void RemoveEvent()
    {
    }

    public override void FindElement()
    {
        _exitBtn = _root.Q<VisualElement>("exit-btn");
        _container = _root.Q<VisualElement>("contents");
        List<VisualElement> fishElements = _root.Query<VisualElement>(className: "fish-element").ToList();
        for(int i = 0; i < fishElements.Count; i++){
            _fishes.Add(new DictionaryUnit(fishElements[i]));
        }
        VisualElement detailRoot = _root.Q<VisualElement>("content-detail");
        _detailView = new DictionaryDetail(detailRoot, _container);
    }
}
