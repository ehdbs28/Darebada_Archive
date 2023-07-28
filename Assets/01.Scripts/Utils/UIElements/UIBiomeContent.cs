using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBiomeContent
{
    private OceneType _type;

    private VisualElement _rootElement;
    private VisualElement _movetoBtn; 

    public UIBiomeContent(VisualElement root, OceneType type){
        _type = type;
        _rootElement = root;

        _movetoBtn = root.Q<VisualElement>("moveto-btn");

        BiomeData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.BiomeData) as BiomeData;
        bool state = data.Biomes[(int)_type];

        AddEvent();
        Lock(!state);
    }

    public void Lock(bool value){
        if(value){  
            _rootElement.RemoveFromClassList("selected");
            _rootElement.AddToClassList("lock");
        }
        else{
            _rootElement.RemoveFromClassList("selected");
            _rootElement.RemoveFromClassList("lock");
        }
    }

    private void AddEvent(){
        _movetoBtn.RegisterCallback<ClickEvent>(e => {
            // 바이옴 이동
            Debug.Log($"{_type}으로 이동");
        });
    }
}
