using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DataRemoveCheckPopup : UIPopup
{
    private UIPopup _basePopup;
    
    private VisualElement _exitBtn;
    private VisualElement _enterBtn;
    
    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e =>
        {
            RemoveRoot();
        });
        
        _enterBtn.RegisterCallback<ClickEvent>(e =>
        {
            GameManager.Instance.GetManager<DataManager>().ResetData();
            GameManager.Instance.GetManager<AquariumNumericalManager>().ResetManager();
            base.RemoveRoot();
            _basePopup.RemoveRoot();

            GameManager.Instance.OnTitle = false;
            GameManager.Instance.GetManager<GameSceneManager>().ChangeScene(GameSceneType.Camp);
        });
    }

    public void SetBasePopup(UIPopup popup)
    {
        _basePopup = popup;
    }

    public override void RemoveEvent()
    {
        
    }

    public override void FindElement()
    {
        _exitBtn = _root.Q("exit-btn");
        _enterBtn = _root.Q("enter-btn");
    }

    public override void RemoveRoot()
    {
        if(_documentRoot == null || _root == null){
            return;
        }

        RemoveEvent();

        _documentRoot.Remove(_root);

        _documentRoot = null;
        _root = null;
        _blurPanel = null;

        _isOpenPopup = false;
    }
}
