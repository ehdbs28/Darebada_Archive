using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventoryGrid selectedItemGrid;

    InventoryItem selectedItem;
    InventoryItem overlapItem;
    RectTransform rectTransform;

    [SerializeField] List<ItemSO> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    [SerializeField] Button inventoryRightRotateBtn;
    [SerializeField] Button inventoryLeftRotateBtn;

    Vector2Int before;
    InventoryItem beforeItem;

    private void Start()
    {
        inventoryLeftRotateBtn.onClick.AddListener(() => RotateItem(-1));
        inventoryRightRotateBtn.onClick.AddListener(() => RotateItem(1));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateRandomItem();
        }

        if (selectedItemGrid == null) return; // �κ��丮 ĭ�� ���ٸ� ���� �Ƚ�Ű�� ����

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }
    }
    

    private void RotateItem(int clockwise = 1) // ������ ȸ��
    {
        if (selectedItem == null) { return; } // ���� �������� ���ٸ� ���� ����

        before = new Vector2Int(selectedItem.onGridPositionX, selectedItem.onGridPositionY);
        beforeItem = selectedItem;

        selectedItem.Rotate(clockwise);

        PlaceItem(before);
        selectedItem = beforeItem;
        PickUpItem(before);
    }

    private void CreateRandomItem() // ���� ������ ����
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem; // ������ ������ ����

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }

    private void LeftMouseButtonPress()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null) // �������� �����ٸ�
        {
            position.x -= (selectedItem.WIDTH - 1) * InventoryGrid.tileSizeWidth / 2;
            position.y += (selectedItem.HEIGHT - 1) * InventoryGrid.tileSizeHeight / 2;
        }

        Vector2Int tileGridPosition = selectedItemGrid.GetTileGridPosition(position); // ���콺 ��ġ �޾ƿ��� 

        if (selectedItem == null) // �������� �������ٸ�
        {
            PickUpItem(tileGridPosition); // ������ ����
        }
        else // �������� �����ٸ�
        {
            PlaceItem(tileGridPosition); // ������ ��ġ ���ϱ�
        }
    }

    private void PlaceItem(Vector2Int tileGridPosition) // ������ ��ġ �α�
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem); // ���� �������� �ξ��°�

        if (complete) // ���� �������� �״ٸ�
        {
            selectedItem = null; // ���� ������ ���ֱ�
            if (overlapItem != null) // �ߺ��� ��ġ�� �������� �ִٸ�
            {
                selectedItem = overlapItem; // �տ� ����
                overlapItem = null; // �ߺ��� ������ ���ֱ�
                rectTransform = selectedItem.GetComponent<RectTransform>(); // �̵� ��Ű���� transform �޾ƿ���
            }
        }

    }

    private void PickUpItem(Vector2Int tileGridPosition) // ������ ���� ���� �����ٸ� �̵��� ���� transform ��������
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }
}
