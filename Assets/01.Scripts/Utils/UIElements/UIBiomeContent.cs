using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBiomeContent
{
    private OceanType _type;

    private VisualElement _rootElement;
    private VisualElement _movetoBtn;

    private BiomeSelectPopup _parentPopup;

    public UIBiomeContent(VisualElement root, BiomeSelectPopup biomeSelectPopup, OceanType type){
        _type = type;
        _rootElement = root;
        _parentPopup = biomeSelectPopup;

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
            _parentPopup.RemoveRoot();
            GameManager.Instance.GetManager<OceanManager>().SetType(_type);
            GameManager.Instance.GetManager<GameSceneManager>().ChangeScene(GameSceneType.Ocean);
        });
    }
}
