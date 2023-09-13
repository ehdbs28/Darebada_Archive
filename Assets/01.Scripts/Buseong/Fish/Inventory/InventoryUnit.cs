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
    public Vector2[] rotateVals = new Vector2[4];

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

        rotateVals[0] = new Vector2(size.x, size.y);
        rotateVals[1] = new Vector2(-size.y, -size.x);
        rotateVals[2] = new Vector2(-size.x, -size.y);
        rotateVals[3] = new Vector2(-size.y, size.x);
    }

    public void Generate(VisualElement root)
    {
        _root = root;
        _inner = _root.Q<VisualElement>("inner");
        Setting();
    }
    
    public void Setting()
    {
        _root.style.width = new StyleLength(new Length(size.x * 15.25f, LengthUnit.Percent));
        _root.style.height = new StyleLength(new Length(11.5f + (size.y * 11.6f), LengthUnit.Percent));

        _inner.style.width = new StyleLength(new Length(size.x * 15.25f, LengthUnit.Percent));
        _inner.style.height = new StyleLength(new Length(11.5f + (size.y * 11.6f), LengthUnit.Percent));

        Move(new Vector2(posX, posY));
        Selected(false);

        _root.style.backgroundImage = new StyleBackground(data.Visual.Profile);
    }

    public void Selected(bool select)
    {
        _inner.style.opacity = select ? 1 : 0;
    }

    public void Rotate(float val)
    {
        _root.style.rotate = new Rotate(_root.style.rotate.value.angle.value + val);
    }

    public void Move(Vector2 pos)
    {
        posX = (int)pos.x;
        posY = (int)pos.y;
        _root.style.left = pos.x * 134.5f + 45;
        _root.style.top = pos.y * 122 + 36;
    }
}
