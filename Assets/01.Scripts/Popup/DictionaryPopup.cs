using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DictionaryPopup : UIPopup
{
    [SerializeField]
    private VisualTreeAsset _dictionatyUnit;
    [SerializeField]
    private VisualTreeAsset _fishRarityStar;
    
    private VisualElement _exitBtn;
    private VisualElement _container;

    private DictionaryDetail _detailView;

    private List<DictionaryUnit> _fishes = new List<DictionaryUnit>();

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
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

        var dataTable = (FishDataTable)GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.FishData);
        var biomeRoots = _root.Query(className: "dictionary-biome").ToList()
            .Select(b => b.Q("fishes").Q("fish-container")).ToList();
        
        for (int i = 0; i < dataTable.Size; i++)
        {
            VisualElement unitRoot = _dictionatyUnit.Instantiate().Q("FishUnit");
            var unit = dataTable.DataTable[i];
            _fishes.Add(new DictionaryUnit(unitRoot, _fishRarityStar, unit));
            biomeRoots[(int)unit.Habitat].Add(_fishes[i].Root);
        }

        VisualElement detailRoot = _root.Q<VisualElement>("content-detail");
        _detailView = new DictionaryDetail(detailRoot, _container);
    }
}
