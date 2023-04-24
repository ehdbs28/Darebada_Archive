using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance = null;

    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _inventorySelect;

    [SerializeField] private List<Button> _inventorySlots;

    public Inventory _item;
    public Inventory _beforeItem;
    public bool _isChange = false;

    private Image _inventorySelectImage;

    private bool _isInventoryActive = false;
    private bool _isSelectActive = false;

    private int _activeSlotIdx = 4; // 초기에 켜놓을 슬롯의 개수를 리스트로 바꾸어 인덱스로 표현하기
    private int _slotIncreaseValue = 5; // 몇 칸씩 열 것인지

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
   
        _inventorySelectImage = _inventorySelect.transform.GetChild(0).GetComponentInChildren<Image>();
    }

    private void Start()
    {
        _inventory.SetActive(false);
        _inventorySelect.SetActive(false);
        
        _isInventoryActive = false;
        _isSelectActive = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseInventory();
        }
    }

    public void OnInventory()
    {
        _isInventoryActive = !_isInventoryActive;
        _inventory.SetActive(_isInventoryActive);
    }

    public void OnInventorySelect(Image selectImg, Inventory item)
    {
        if (_inventorySelect.activeSelf)
        {
            if (_beforeItem == item)
            {
                _isSelectActive = false;
            }
            else
            {
                _isSelectActive = true;
                _beforeItem = item;
            }
            _inventorySelect.SetActive(_isSelectActive);
        }
        else
        {
            _beforeItem = item;
            _inventorySelect.SetActive(true);
        }
        _item = item;
        _inventorySelectImage.sprite = selectImg.sprite;
    }

    public void IncreaseInventory()
    {
        if (_inventorySlots.Count > _activeSlotIdx + 1)
        {
            for (int i = 1; i <= _slotIncreaseValue; i++)
            {
                _inventorySlots[_activeSlotIdx + i].gameObject.SetActive(true);
            }
            _activeSlotIdx += _slotIncreaseValue;
        }
        else
        {
            print("There are no ready slots in the inventory slot list");
        }
    }
}
