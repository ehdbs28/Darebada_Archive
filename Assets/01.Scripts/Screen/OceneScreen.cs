using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OceneScreen : UIScreen
{
    private Label _timeText;
    private Label _dateText;

    private VisualElement _settingBtn;
    private VisualElement _gobackBtn;
    private VisualElement _letterBtn;
    private VisualElement _dictionaryBtn;
    private VisualElement _inventoryBtn;

    public override void AddEvent(VisualElement root)
    {
        _settingBtn.RegisterCallback<ClickEvent>(e => {
            Debug.Log("세팅 클릭");
        });

        _gobackBtn.RegisterCallback<ClickEvent>(e => {
            Debug.Log("집으로 클릭");
        });

        _letterBtn.RegisterCallback<ClickEvent>(e => {
            Debug.Log("편지 클릭");
        });

        _dictionaryBtn.RegisterCallback<ClickEvent>(e => {
            Debug.Log("도감 클릭");
        });

        _inventoryBtn.RegisterCallback<ClickEvent>(e => {
            Debug.Log("인벤 클릭");
        });
    }

    public override VisualElement GenerateRoot()
    {
        VisualElement root = _treeAsset.Instantiate();
        root = root.Q<VisualElement>("container");

        _settingBtn = root.Q<VisualElement>("setting-btn");
        _gobackBtn = root.Q<VisualElement>("goto-btn");
        _letterBtn = root.Q<VisualElement>("letter-btn");
        _dictionaryBtn = root.Q<VisualElement>("dictionary-btn");
        _inventoryBtn = root.Q<VisualElement>("inventory-btn");

        return root;
    }
}
