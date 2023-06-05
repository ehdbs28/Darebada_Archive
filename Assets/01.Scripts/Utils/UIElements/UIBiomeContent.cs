using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum BiomeContentState{
    LOCK = 0,
    UNSELECT,
    SELECT
}

public class UIBiomeContent
{
    private BiomeContentState _contentState = BiomeContentState.LOCK;

    private VisualElement _rootElement;
    private VisualElement _movetoBtn; 

    public UIBiomeContent(VisualElement root){
        _rootElement = root;

        _movetoBtn = root.Q<VisualElement>("moveto-btn");
        AddEvent();
        SetContentState(BiomeContentState.LOCK);
    }

    public void SetContentState(BiomeContentState state){
        _contentState = state;

        if(state == BiomeContentState.LOCK){
            _rootElement.RemoveFromClassList("selected");
            _rootElement.AddToClassList("lock");
        }
        else if(state == BiomeContentState.UNSELECT){
            _rootElement.RemoveFromClassList("selected");
            _rootElement.RemoveFromClassList("lock");
        }
        else if(state == BiomeContentState.SELECT){
            _rootElement.RemoveFromClassList("lock");
            _rootElement.AddToClassList("selected");
        }
    }

    private void AddEvent(){
        _movetoBtn.RegisterCallback<ClickEvent>(e => {
            
        });
    }
}
