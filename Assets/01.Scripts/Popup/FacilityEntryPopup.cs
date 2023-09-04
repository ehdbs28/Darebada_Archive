using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FacilityEntryPopup : UIPopup
{
    private VisualElement _exitBtn;
    private VisualElement _enterBtn;
    private Label _facilityLabel;

    public PopupType popupType;
    public GameSceneType sceneType;

    public bool isPopup;

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        base.SetUp(document, clearScreen, blur, timeStop);
    }

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });

        _enterBtn.RegisterCallback<ClickEvent>(e =>
        {
            RemoveRoot();
            if (isPopup)
            {
                GameManager.Instance.GetManager<UIManager>().ShowPanel(popupType);
            }
            else
            {
                GameManager.Instance.GetManager<GameSceneManager>().ChangeScene(sceneType);
            }
        });
    }

    public override void FindElement()
    {
        _exitBtn = _root.Q<VisualElement>("exit-btn");
        _enterBtn = _root.Q<VisualElement>("enter-btn");
        _facilityLabel = _root.Q<Label>("facility-label");
    }

    public override void RemoveEvent()
    {
        
    }
}
