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

    protected override void AddEvent(VisualElement root)
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

    protected override void FindElement(VisualElement root)
    {
        _exitBtn = root.Q<VisualElement>("exit-btn");
        _container = root.Q<VisualElement>("contents");
        List<VisualElement> fishElements = root.Query<VisualElement>(className: "fish-element").ToList();
        for(int i = 0; i < fishElements.Count; i++){
            _fishes.Add(new DictionaryUnit(fishElements[i]));
        }
        VisualElement detailRoot = root.Q<VisualElement>("content-detail");
        _detailView = new DictionaryDetail(detailRoot, _container);
    }
}
