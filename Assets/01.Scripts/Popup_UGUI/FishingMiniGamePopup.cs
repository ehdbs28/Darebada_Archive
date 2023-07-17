using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FishingMiniGamePopup : UGUIPopup
{
    [SerializeField]
    private Transform _value;

    [SerializeField]
    private RectTransform _cursor;

    private float _cursorRadius = 105;

    [SerializeField]
    private float _angle = 0f;
    public float Angle => _angle % 360;

    [SerializeField]
    private float _cursorMinSpeed = 30f;

    [SerializeField]
    private float _cursorMaxSpeed = 50f;

    private float _cursorSpeed;

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        base.SetUp(document, clearScreen, blur, timeStop);
        _angle = 0f;
        _cursorSpeed = Random.Range(_cursorMinSpeed, _cursorMaxSpeed);
        SetCursorPosition(_angle);
    }

    public override void GenerateRoot()
    {
        base.GenerateRoot();
        GameManager.Instance.GetManager<MiniGameManager>().SetUp(_value);
    }

    public override void RemoveRoot()
    {
        GameManager.Instance.GetManager<MiniGameManager>().Clear(_value);
        base.RemoveRoot();
    }

    private void Update() {
        if(_value.childCount <= 0)
            return;

        _angle += _cursorSpeed * Time.deltaTime;

        SetCursorPosition(_angle);
    }

    public override void AddEvent()
    {
    }

    public override void RemoveEvent()
    {
    }

    public override void FindElement()
    {
    }

    private void SetCursorPosition(float angle){
        float rad = (90 - angle) * Mathf.Deg2Rad;
        Vector3 pos = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)) * _cursorRadius;
        Quaternion rot = Quaternion.Euler(0, 0, 180 - angle);

        _cursor.anchoredPosition = pos;
        _cursor.rotation = rot;
    }
}
