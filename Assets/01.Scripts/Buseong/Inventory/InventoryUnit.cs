using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUnit
{
    private int _width;
    public int Width;
    private int _height;
    public int Height;

    private int _posX;
    public int PosX
    {
        get => _posX;
        set => _posX = value;
    }
    private int _posY;
    public int PosY
    {
        get => _posY;
        set => _posY = value;
    }
    private int _rotate;
    public int Rotate
    {
        get => _rotate;
        set => _rotate = value;
    }
    public int[,] _rotateVals = 
    {   
        { 3, 2 }, 
        { -2, 3 }, 
        { -3, -2 }, 
        { 2, -3 } 
    };

    private VisualTreeAsset _template;

    private VisualElement _parent;
    public VisualElement Root;

    private FishDataUnit _data;

    public InventoryUnit(VisualTreeAsset template, VisualElement parent, FishDataUnit data)
    {
        _template = template;
        _parent = parent;
        _data = data;
    }

    public void Generate(InventoryUnit unit)
    {
        Root = _template.Instantiate();
        Root = Root.Q<VisualElement>("inventory-unit");
        _parent.Add(Root);

        FindElement();
        Setting(unit); //얘 내용 집어넣어야함
        AddEvent();
    }

    public int GetPosX() { return _posX; }
    public int GetPosY() { return _posY; }

    public int GetMinX()
    {
        int minX = Mathf.Min(_posX + _rotateVals[_rotate, 0], _posX);
        return minX;
    }

    public int GetMaxX()
    {
        int maxX = Mathf.Max(_posX + _rotateVals[_rotate, 0], _posX);
        return maxX;
    }

    public int GetMinY()
    {
        int minY = Mathf.Min(_posY + _rotateVals[_rotate, 1], _posY);
        return minY;
    }

    public int GetMaxY()
    {
        int maxY = Mathf.Max(_posY + _rotateVals[_rotate, 1], _posY);
        return maxY;
    }

    public int GetSize() { return (_rotateVals[0, 0] + 1) * (_rotateVals[0, 1] + 1); }
    
    public void Setting(InventoryUnit unit)
    {
        //_root.style.width = _width;
        //_root.style.height = _height;
        Root.style.width = 300;
        Root.style.height = 200;
        
        _posX = unit.PosX;
        _posY = unit.PosY;
        _rotate = unit.Rotate;

        Root.style.left = _posX * 100 + 10;
        Root.style.top = _posY * 100 + 10;

        //_root.style.backgroundImage = new StyleBackground(_data.Visual.Profile);
    }

    private void FindElement()
    {
        
    }

    private void AddEvent()
    {
        Root.RegisterCallback<ClickEvent>(e =>
        {
            ((InventoryPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Inventory))._selectedUnit = this;
            //((InventoryPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Inventory))._selectedUnitProfile.style.backgroundImage = (Texture2D)Resources.Load("05.Assets/FishIcon/BlueTang");
            Debug.Log("ㅁㄴㅇㄹ");
        });
    }

    public void Remove()
    {
        _parent.Remove(Root);
    }
}
