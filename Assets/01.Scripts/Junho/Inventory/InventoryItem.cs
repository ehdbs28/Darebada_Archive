using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemSO itemData;

    public int HEIGHT
    {
        get
        {
            if (rotated == false)
            {
                return itemData.height;
            }
            return itemData.width;
        }
    }

    public int WIDTH
    {
        get
        {
            if (rotated == false)
            {
                return itemData.width;
            }
            return itemData.height;
        }
    }

    public int onGridPositionX;
    public int onGridPositionY;

    public bool rotated = false;


    internal void Set(ItemSO itemData) // �κ��丮 ������ �̹����� ������ ����
    {
        this.itemData = itemData;

        GetComponent<Image>().sprite = itemData.itemicon;

        Vector2 size = new Vector2();
        size.x = itemData.width * InventoryGrid.tileSizeWidth;
        size.y = itemData.height * InventoryGrid.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }

    public void Rotate(int clockwise = 1) // �κ��丮 ������ ȸ�� 90�� 0���� �ݺ�
    {
        rotated = !rotated;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotated == true ? clockwise * 90f : 0f);
    }
}
