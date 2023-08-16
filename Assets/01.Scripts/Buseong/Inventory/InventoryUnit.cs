using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class InventoryUnit
{
    public int width;
    public int height;

    public int posX;
    public int posY;
    public int rotate;

    public Vector2 size;
    
    public FishDataUnit data;
    
    private int[,] _rotateVals = 
    {   
        { 3, 2 }, 
        { -2, 3 }, 
        { -3, -2 }, 
        { 2, -3 } 
    };

    private VisualElement _root;
    
    public int MinX => Mathf.Min(posX + _rotateVals[rotate, 0], posX);
    public int MaxX => Mathf.Max(posX + _rotateVals[rotate, 0], posX);
    public int MinY => Mathf.Min(posY + _rotateVals[rotate, 1], posY);
    public int MaxY => Mathf.Max(posY + _rotateVals[rotate, 1], posY);

    public InventoryUnit(FishDataUnit data, Vector2 size)
    {
        this.data = data;
        this.size = size;
    }

    public void Generate(VisualElement root)
    {
        _root = root;
        
        Setting();
        AddEvent();
    }


    // public int GetSize() { return (_rotateVals[0, 0] + 1) * (_rotateVals[0, 1] + 1); }
    
    public void Setting()
    {
        // 값 저장하고 넣어줘야 함
        posX = 0;
        posY = 0;
        rotate = 0;
        
        _root.style.width = 300;
        _root.style.height = 200;

        _root.style.left = posX * 100 + 10;
        _root.style.top = posY * 100 + 10;

        //_root.style.backgroundImage = new StyleBackground(data.Visual.Profile);
    }

    public void Rotate(float val)
    {
        _root.style.rotate = new Rotate(_root.style.rotate.value.angle.value + val);
    }

    public void Move(Vector2 pos)
    {
        posX = (int)pos.x;
        posY = (int)pos.y;
        _root.style.left = pos.x * 100 + 10;
        _root.style.top = pos.y * 100 + 10;
    }

    private void FindElement()
    {
        
    }

    private void AddEvent()
    {
        _root.RegisterCallback<ClickEvent>(e =>
        {
            ((InventoryPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Inventory)).selectedUnit = this;
        });
    }
}
