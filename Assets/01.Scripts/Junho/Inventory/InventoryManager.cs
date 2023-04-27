using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryGrid selectedItemGrid;

    InventoryItem selectedItem;
    InventoryItem overlapItem;
    RectTransform rectTransform;

    [SerializeField] List<ItemSO> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    [SerializeField] GameObject inventorySelectPanel;
    [SerializeField] GameObject inventoryMovePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateRandomItem();
        }

        if (selectedItemGrid == null) return; // 인벤토리 칸이 없다면 실행 안시키기 위함

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }
    }

    private void RotateItem() // 아이템 회전
    {
        if (selectedItem == null) { return; } // 집은 아이템이 없다면 실행 안함

        selectedItem.Rotate();
    }

    private void CreateRandomItem() // 랜덤 아이템 생성
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem; // 아이템 집었다 판정

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }

    private void LeftMouseButtonPress()
    {
        Vector2 position = Input.mousePosition;

        

        if (selectedItem != null) // 아이템을 집었다면
        {
            position.x -= (selectedItem.WIDTH - 1) * InventoryGrid.tileSizeWidth / 2;
            position.y += (selectedItem.HEIGHT - 1) * InventoryGrid.tileSizeHeight / 2;
        }

        Vector2Int tileGridPosition = selectedItemGrid.GetTileGridPosition(position); // 마우스 위치 받아오기 

        if (selectedItem == null) // 아이템을 안집었다면
        {
            PickUpItem(tileGridPosition); // 아이템 집기
        }
        else // 아이템을 집었다면
        {
            PlaceItem(tileGridPosition); // 아이템 위치 정하기
        }
    }

    private void PlaceItem(Vector2Int tileGridPosition) // 아이템 위치 두기
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem); // 집은 아이템을 두었는가

        if (complete) // 집은 아이템을 뒀다면
        {
            selectedItem = null; // 집은 아이템 없애기
            if (overlapItem != null) // 중복된 위치에 아이템이 있다면
            {
                selectedItem = overlapItem; // 손에 집기
                overlapItem = null; // 중복될 아이템 없애기
                rectTransform = selectedItem.GetComponent<RectTransform>(); // 이동 시키도록 transform 받아오기
            }
        }

    }

    private void PickUpItem(Vector2Int tileGridPosition) // 아이템 집기 만약 집었다면 이동을 위한 transform 가져오기
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }
}
