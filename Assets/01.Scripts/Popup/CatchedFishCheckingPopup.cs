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

    public void SetData(FishDataUnit data)
    {
        _nameText.text = data.Name;
        _fishImage.style.backgroundImage = new StyleBackground(data.Visual.Profile);
        //���� ���� �� ����Ǿ��ִ°� �����ؾ���
        _weightText.text = (Random.Range(data.MinWeight, data.MaxWeight)).ToString();
        _lengthText.text = (Random.Range(data.MinLenght, data.MaxLenght)).ToString();
        _description.text = data.Description;
    }

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        base.SetUp(document, clearScreen, blur, timeStop);
        SetData(dataUnit);
    }

    public override void AddEvent()
    {
        _releaseBtn.RegisterCallback<ClickEvent>(e =>
        {
            RemoveRoot();
        });

        _captureBtn.RegisterCallback<ClickEvent>(e =>
        {
            //������ �Է��ؼ� �κ��丮�� �־������
            Debug.Log("������ �Է�");
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
