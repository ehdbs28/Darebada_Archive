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
    
    public int MinX => Mathf.Min(posX + (int)rotateVals[rotate].x, posX);
    public int MaxX => Mathf.Max(posX + (int)rotateVals[rotate].x, posX);
    public int MinY => Mathf.Min(posY + (int)rotateVals[rotate].y, posY);
    public int MaxY => Mathf.Max(posY + (int)rotateVals[rotate].y, posY);

    public InventoryUnit(FishDataUnit data, Vector2 size)
    {
        this.data = data;
        this.size = size;
        this.size = new Vector2(3, 2);
    }

    public void Generate(VisualElement root)
    {
        _root = root;
        Setting();
    }
    
    public void Setting()
    {
        _root.style.width = size.x * 100;
        _root.style.height = size.y * 100;
        
        Move(new Vector2(posX, posY));

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
}
