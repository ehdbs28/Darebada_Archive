using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid : MonoBehaviour
{//
    public const float tileSizeWidth = 32;  // Ÿ�� ����
    public const float tileSizeHeight = 32; // Ÿ�� ����

    private InventoryItem[,] inventoryItemSlot; // �κ��丮 ĭ�� �ְų� ���ų� ��ġ�°� Ȯ���ϱ� ����

    private RectTransform rectTransform;

    public float gridSizeWidth = 7; // �ʱ� �κ��丮 ���ΰ� ������ �ִ� 7
    public float gridSizeHeight = 1; // �ʱ� �κ��丮 ���ΰ� ������ �ִ� 7

    private void Start() // �ʱ�ȭ
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
    }

    public void Init(float width, float height) // �κ��丮 ũ�� ���� // �κ��丮 ����� ���ϸ� �ٽ� �ҷ������
    {
        inventoryItemSlot = new InventoryItem[(int)width, (int)height]; // �� ������ �κ��丮�� ������ �־���
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight); // �ȼ� ���� ������ ������
        rectTransform.sizeDelta = size;
    }
    private void CleanGridReference(InventoryItem item) // item�� ��ġ���� item ������ ��ŭ ����
    {
        for (int ix = 0; ix < item.WIDTH; ix++) // �������� ���α���
        {
            for (int iy = 0; iy < item.HEIGHT; iy++) // �������� ���α���
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null; // �� ĭ�� �ִ� ������ �����
            }
        }
    }

    private Vector2 positionOnTheGrid = new Vector2();
    private Vector2Int tileGridPosition = new Vector2Int();

    public Vector2Int GetTileGridPosition(Vector2 mousePosition) // ���콺�� �κ��丮 Ÿ���� ����ִ��� Ȯ���ϴ� ��
    {
        // ���콺 ��ġ
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        // ���� ���� �������� ���ĸ� ������ ���ϴ� ��
        tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);

        return tileGridPosition;
    }

    public InventoryItem PickUpItem(int x, int y) // �������� ���´�
    {
        InventoryItem toReturn = inventoryItemSlot[x, y]; // x,y ��ġ�� �������� ��´�

        if (toReturn == null) { return null; } // ���� �������� ������ ���� ����

        CleanGridReference(toReturn); // ���� ������ ��ġ ����
        return toReturn;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem) // ������ �α�
    {
        if (BoundryCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT) == false) // ������ ����ٸ� ���� ����
            return false;

        if (OverlapCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT, ref overlapItem) == false) // �ߺ��̶�� ���� ����
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null) // ��ģ �������� �ִٸ� ����ֱ�
        {
            CleanGridReference(overlapItem);
        }

        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform); // �ڽ����� �־��ֱ�

        for (int x = 0; x < inventoryItem.WIDTH; x++)
        {
            for (int y = 0; y < inventoryItem.HEIGHT; y++)
            {
                // x,y���� ������ ��ŭ ���� ���������� �ٲ��ش�
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        // ������ ��ġ�� �������ֱ�
        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.WIDTH / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.HEIGHT / 2);

        rectTransform.localPosition = position;

        // ������ �δ� ������ �������̶��
        return true;
    }

    public bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem) // �ߺ� üũ
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null) // �������� �ִٸ�
                {
                    overlapItem = inventoryItemSlot[posX + x, posY + y]; // overlap���� �ش� �������� ����
                }
                else // �������� ���ٸ�
                {
                    if (overlapItem != inventoryItemSlot[posX + x, posY + y]) // �ٸ� �������̶��
                    {
                        return false;
                    }
                }
            }
        }

        // �������� �ְų�// �������� ���� ���� �������̶�� 
        return true;
    }

    private bool PositionCheck(int posX, int posY) // ���� ��, �κ��丮 ���� üũ
    {
        if (posX < 0 || posY < 0) // ���� ���� ����°�?
            return false;

        if (posX >= gridSizeWidth || posY >= gridSizeHeight) // �κ��丮�� ����°�?
            return false;

        return true;
    }

    private bool BoundryCheck(int posX, int posY, int width, int height) // �κ��丮 ������ Ȯ���ϱ�
    {
        if (PositionCheck(posX, posY) == false) // ���� �κ��丮 ���ΰ�?
            return false;

        posX += width - 1;
        posY += height - 1;

        if (PositionCheck(posX, posY) == false) // ���� ������� �� ���ΰ�?
            return false;

        return true;
    }
}
