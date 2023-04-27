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


    internal void Set(ItemSO itemData) // 인벤토리 아이템 이미지와 사이즈 설정
    {
        this.itemData = itemData;

        GetComponent<Image>().sprite = itemData.itemicon;

        Vector2 size = new Vector2();
        size.x = itemData.width * InventoryGrid.tileSizeWidth;
        size.y = itemData.height * InventoryGrid.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }

    public void Rotate(int clockwise = 1) // 인벤토리 아이템 회전 90도 0도를 반복
    {
        rotated = !rotated;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotated == true ? clockwise * 90f : 0f);
    }
}
