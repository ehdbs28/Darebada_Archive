using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CatchedFishCheckingPopup : UIPopup
{
    private VisualElement _releaseBtn;
    private VisualElement _captureBtn;

    private Label _nameText;

    private VisualElement _fishImage;

    private Label _catchedNumText;
    private Label _weightText;
    private Label _lengthText;
    private Label _description;

    public FishDataUnit dataUnit;

    public void SetData()
    {
        _nameText.text = dataUnit.Name;
        
        DictionaryData dicData = GameManager.Instance.GetManager<DataManager>()
            .GetData(DataType.DictionaryData) as DictionaryData;
        var unit = dicData.Units.List.Find(unit => unit.Name == dataUnit.Name);

        if (unit != null)
            _catchedNumText.text = $"{unit.Count}마리";

        _fishImage.style.backgroundImage = new StyleBackground(dataUnit.Visual.Profile);
        _weightText.text = (Mathf.Round(Random.Range(dataUnit.MinWeight, dataUnit.MaxWeight) * 10) / 10).ToString();
        _lengthText.text = (Mathf.Round(Random.Range(dataUnit.MinLenght, dataUnit.MaxLenght) * 10) / 10).ToString();
        _description.text = dataUnit.Description;
    }

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        base.SetUp(document, clearScreen, blur, timeStop);
        SetData();
    }

    public override void AddEvent()
    {
        _releaseBtn.RegisterCallback<ClickEvent>(e =>
        {
            RemoveRoot();
        });

        _captureBtn.RegisterCallback<ClickEvent>(e =>
        {
            Vector2 size = new Vector2(dataUnit.InvenSizeX, dataUnit.InvenSizeY);
            GameManager.Instance.GetManager<InventoryManager>().AddUnit(dataUnit, size);
            RemoveRoot();
        });
    }

    public override void FindElement()
    {
        _releaseBtn = _root.Q<VisualElement>("release-btn");
        _captureBtn = _root.Q<VisualElement>("capture-btn");

        _nameText = _root.Q<Label>("name-text");

        _fishImage = _root.Q<VisualElement>("fish-image");

        _catchedNumText = _root.Q<Label>("catch-num-text");
        _weightText = _root.Q<Label>("weight-value-text");
        _lengthText = _root.Q<Label>("length-text");
        _description = _root.Q<Label>("description-text");
    }

    public override void RemoveEvent()
    {

    }
}
