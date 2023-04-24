using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private Image _itemImage;
    private Button _itemButton;

    private void Awake()
    {
        _itemImage = GetComponent<Image>();
        _itemButton = GetComponent<Button>();
    }

    private void Start()
    {
        _itemButton.onClick.AddListener(SelectSlot);
        _itemButton.onClick.AddListener(ChangeSlot);
    }

    private void Update()
    {
        UpdateSlot();
    }

    private void UpdateSlot()
    {

    }
    
    private void ChangeSlot()
    {
        Sprite temp = this._itemImage.sprite;
        if (InventoryManager.Instance._isChange)
        {
            this._itemImage.sprite = InventoryManager.Instance._beforeItem._itemImage.sprite;
            InventoryManager.Instance._beforeItem._itemImage.sprite = temp;

            InventoryManager.Instance._isChange = false;
        }
    }

    private void SelectSlot() // 인벤토리 클릭
    {
        if (!InventoryManager.Instance._isChange)
        {
            InventoryManager.Instance.OnInventorySelect(this._itemImage, this);
        }
    }

}
