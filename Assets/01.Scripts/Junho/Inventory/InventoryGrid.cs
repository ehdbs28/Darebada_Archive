using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid : MonoBehaviour
{//
    public const float tileSizeWidth = 32;  // 타일 가로
    public const float tileSizeHeight = 32; // 타일 세로

    private InventoryItem[,] inventoryItemSlot; // 인벤토리 칸에 넣거나 집거나 겹치는거 확인하기 위함

    private RectTransform rectTransform;

    public float gridSizeWidth = 7; // 초기 인벤토리 가로가 몇인지 최대 7
    public float gridSizeHeight = 1; // 초기 인벤토리 세로가 몇인지 최대 7

    private void Start() // 초기화
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
    }

    public void Init(float width, float height) // 인벤토리 크기 조절 // 인벤토리 사이즈가 변하면 다시 불러줘야함
    {
        inventoryItemSlot = new InventoryItem[(int)width, (int)height]; // 값 상으로 인벤토리가 몇인지 넣어줌
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight); // 픽셀 수와 곱해준 사이즈
        rectTransform.sizeDelta = size;
    }
    private void CleanGridReference(InventoryItem item) // item의 위치에서 item 사이즈 만큼 비우기
    {
        for (int ix = 0; ix < item.WIDTH; ix++) // 아이템의 가로길이
        {
            for (int iy = 0; iy < item.HEIGHT; iy++) // 아이템의 세로길이
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null; // 이 칸에 있는 아이템 지우기
            }
        }
    }

    private Vector2 positionOnTheGrid = new Vector2();
    private Vector2Int tileGridPosition = new Vector2Int();

    public Vector2Int GetTileGridPosition(Vector2 mousePosition) // 마우스가 인벤토리 타일이 어디있는지 확인하는 것
    {
        // 마우스 위치
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        // 왼쪽 위를 기준으로 몇컴마 몇인지 구하는 것
        tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);

        return tileGridPosition;
    }

    public InventoryItem PickUpItem(int x, int y) // 아이템을 집는다
    {
        InventoryItem toReturn = inventoryItemSlot[x, y]; // x,y 위치의 아이템을 잡는다

        if (toReturn == null) { return null; } // 집은 아이템이 없으면 실행 안함

        CleanGridReference(toReturn); // 집은 아이템 위치 비우기
        return toReturn;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem) // 아이템 두기
    {
        if (BoundryCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT) == false) // 범위를 벗어났다면 실행 안함
            return false;

        if (OverlapCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT, ref overlapItem) == false) // 중복이라면 실행 안함
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null) // 겹친 아이템이 있다면 비워주기
        {
            CleanGridReference(overlapItem);
        }

        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform); // 자식으로 넣어주기

        for (int x = 0; x < inventoryItem.WIDTH; x++)
        {
            for (int y = 0; y < inventoryItem.HEIGHT; y++)
            {
                // x,y부터 사이즈 만큼 집은 아이템으로 바꿔준다
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        // 아이템 위치를 지정해주기
        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.WIDTH / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.HEIGHT / 2);

        rectTransform.localPosition = position;

        // 아이템 두는 과정이 성공적이라면
        return true;
    }

    public bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem) // 중복 체크
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null) // 아이템이 있다면
                {
                    overlapItem = inventoryItemSlot[posX + x, posY + y]; // overlap에는 해당 아이템이 담긴다
                }
                else // 아이템이 없다면
                {
                    if (overlapItem != inventoryItemSlot[posX + x, posY + y]) // 다른 아이템이라면
                    {
                        return false;
                    }
                }
            }
        }

        // 아이템이 있거나// 아이템이 없고 같은 아이템이라면 
        return true;
    }

    private bool PositionCheck(int posX, int posY) // 왼쪽 위, 인벤토리 제한 체크
    {
        if (posX < 0 || posY < 0) // 왼쪽 위를 벗어났는가?
            return false;

        if (posX >= gridSizeWidth || posY >= gridSizeHeight) // 인벤토리를 벗어났는가?
            return false;

        return true;
    }

    private bool BoundryCheck(int posX, int posY, int width, int height) // 인벤토리 밖인지 확인하기
    {
        if (PositionCheck(posX, posY) == false) // 현재 인벤토리 밖인가?
            return false;

        posX += width - 1;
        posY += height - 1;

        if (PositionCheck(posX, posY) == false) // 값을 낮춰봤을 때 밖인가?
            return false;

        return true;
    }
}
