using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class InventoryUnit
{
    public int posX;
    public int posY;
    public int rotate;

    public Vector2 size;
    
    [HideInInspector]
    public FishDataUnit data;
    
    [HideInInspector]
    public List<Vector2> rotateVals = new()
    {
        new(3, 2),
        new(-2, 3),
        new(-3, -2),
        new(-2, -3)
    };

    private VisualElement _root;
    public VisualElement Root => _root;
    
    private VisualElement _inner;
    
    public int MinX => Mathf.Min(posX + (int)rotateVals[rotate].x, posX);
    public int MaxX => Mathf.Max(posX + (int)rotateVals[rotate].x, posX);
    public int MinY => Mathf.Min(posY + (int)rotateVals[rotate].y, posY);
    public int MaxY => Mathf.Max(posY + (int)rotateVals[rotate].y, posY);


    public InventoryUnit(FishDataUnit data, Vector2 size)
    {
        this.data = data;
        this.size = size;
    }

    public void Generate(VisualElement root)
    {
        _root = root;
        _inner = _root.Q<VisualElement>("inner");
        Setting();
    }
    
    public void Setting()
    {
        _root.style.width = size.x * 115;
        _root.style.height = size.y * 115;
        _inner.style.width = size.x * 115;
        _inner.style.height = size.y * 115;
        
        Move(new Vector2(posX, posY));

        _root.style.backgroundImage = new StyleBackground(data.Visual.Profile);
    }

    public void Rotate(float val)
    {
        _root.style.rotate = new Rotate(_root.style.rotate.value.angle.value + val);
    }

    public void Move(Vector2 pos)
    {
        posX = (int)pos.x;
        posY = (int)pos.y;
        _root.style.left = pos.x * 115 + 16.5f;
        _root.style.top = pos.y * 115 + 30.5f;
    }
}
