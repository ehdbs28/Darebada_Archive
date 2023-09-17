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

    public void Generate(VisualElement root, bool setting = true, bool shop = false)
    {
        _root = root;
        _inner = _root.Q<VisualElement>("inner");
        Setting(setting, shop);
    }
    
    public void Setting(bool setting, bool shop)
    {
        if (setting && shop == false)
        {
            _root.style.width = new StyleLength(new Length(size.x * 15.9f + 0.2f, LengthUnit.Percent));
            _root.style.height = new StyleLength(new Length(size.y * 12f, LengthUnit.Percent));

            _inner.style.width = new StyleLength(new Length(100f, LengthUnit.Percent));
            _inner.style.height = new StyleLength(new Length(100f, LengthUnit.Percent));

            Move(new Vector2(posX, posY));
            Selected(false);
        }
        else if (shop)
        {
            _root.style.width = new StyleLength(new Length(size.x * 15f, LengthUnit.Percent));
            _root.style.height = new StyleLength(new Length(size.y * 11f, LengthUnit.Percent));

            _inner.style.width = new StyleLength(new Length(100f, LengthUnit.Percent));
            _inner.style.height = new StyleLength(new Length(100f, LengthUnit.Percent));

            Move(new Vector2(posX, posY), shop);
            Selected(false);
        }

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

    public void Move(Vector2 pos, bool shop = false)
    {
        posX = (int)pos.x;
        posY = (int)pos.y;
        Debug.Log($"posX: {posX}");
        Debug.Log($"posY: {posY}");
        if (!shop)
        {
            _root.style.left = new StyleLength(new Length(posX * new Length(15.9f, LengthUnit.Percent).value + 4.2f, LengthUnit.Percent));
            _root.style.top = new StyleLength(new Length(posY * new Length(12f, LengthUnit.Percent).value + 2f, LengthUnit.Percent));
        }
        else
        {
            _root.style.left = new StyleLength(new Length(posX * 15f + 27.1f, LengthUnit.Percent));
            _root.style.top = new StyleLength(new Length(posY * 11f + 4.3f, LengthUnit.Percent));
        }
        Debug.Log($"left: {_root.style.left}");
        Debug.Log($"top: {_root.style.top}");
    }
}
