using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour, IManager
{
    public static InventoryManager Instance;

    //public InventoryGrid selectedItemGrid;

    //[SerializeField] List<ItemSO> items;
    //[SerializeField] GameObject itemPrefab;
    //[SerializeField] Transform canvasTransform;

    //[SerializeField] GameObject inventoryPanel;
    //[Header("inventory Select Panel"), Space(10)]
    //[SerializeField] GameObject inventorySelectPanel;
    //[SerializeField] Image inventorySelectImage;
    //[SerializeField] Button inventoryMoveBtn;
    //[SerializeField] Button inventoryLooseBtn;
    //[SerializeField] Button inventoryDeleteBtn;

    //[Header("inventory Move Panel")]
    //[SerializeField] Button inventoryRightRotateBtn;
    //[SerializeField] Button inventoryLeftRotateBtn;
    //[SerializeField] Image inventoryMoveImage;

    //private InventoryItem selectedItem;
    //private InventoryItem overlapItem;
    //private RectTransform rectTransform;

    private bool isMove = false;
    private bool isActive = false;

    [SerializeField]
    private List<InventoryItem> _itemList;
    public List<InventoryItem> ItemList => _itemList;

    [SerializeField]
    private List<InventoryUnit> _inventoryTileList;
    public List<InventoryUnit> InventoryTileList => _inventoryTileList;

    [SerializeField]
    private List<VisualElement> _unitList;
    public List<VisualElement> UnitList => _unitList;

    public bool _unitMove;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multy InventoryManager");
        }
        Instance = this;
    }

    private void Start()
    {
        //inventoryLeftRotateBtn.onClick.AddListener(() => RotateItem(-1));
        //inventoryRightRotateBtn.onClick.AddListener(() => RotateItem(1));

        //inventoryDeleteBtn.onClick.AddListener(() => {
        //    inventorySelectPanel.SetActive(false);
        //    Vector2Int select = new Vector2Int(selectedItem.onGridPositionX, selectedItem.onGridPositionY);
        //    PlaceItem(select);
        //    selectedItem = null;
        //});

        //inventoryLooseBtn.onClick.AddListener(LooseItem);
        //inventoryMoveBtn.onClick.AddListener(MoveItem);

        // isActive = inventoryPanel.activeSelf;
    }

    private void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    CreateRandomItem();
        //}

        //if (selectedItemGrid == null) return; // �κ��丮 ĭ�� ���ٸ� ���� �Ƚ�Ű�� ����

        //if (Input.GetMouseButtonDown(0))
        //{
        //    LeftMouseButtonPress();
        //    SelectItem(selectedItem);
        //}

        // isActive = !inventoryPanel.activeSelf;
    }

    public void ResetManager()
    {
        _unitList = new List<VisualElement>();
        _inventoryTileList = new List<InventoryUnit>();
    }

    public void InitManager()
    {
        ResetManager();
    }

    public void UpdateManager()
    {

    }

    public List<InventoryUnit> GetTiles()
    {
        return _inventoryTileList;
    }

    public InventoryUnit GetTile(int idx)
    {
        return _inventoryTileList[idx];
    }

    public List<VisualElement> GetUnits()
    {
        return _unitList;
    }

    public VisualElement GetUnit(int idx)
    {
        return _unitList[idx];
    }

    public void AddUnit(VisualElement unit)
    {
        _unitList.Add(unit);
    }

    public void RemoveUnit(VisualElement unit)
    {
        _unitList.Remove(unit);
    }

    public void AddTileEvent(VisualElement selectedUnit)
    {
        for(int i = 0; i < _inventoryTileList.Count; i++)
        {
            _inventoryTileList[i].index = i;
            _inventoryTileList[i].pos = new Vector3(i / 8 * 100, i % 8 * 100);
            _inventoryTileList[i].unit.RegisterCallback<ClickEvent>(e =>
            {
                if (_unitMove)
                {
                    selectedUnit.transform.position = _inventoryTileList[i].pos;
                    Debug.Log(_inventoryTileList[i].pos);
                }
            });
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------

    //private void OnInventory() => inventoryPanel.SetActive(isActive);

    //private void RotateItem(int clockwise = 1) // ������ ȸ��
    //{
    //    Vector2Int before = new Vector2Int(selectedItem.onGridPositionX, selectedItem.onGridPositionY);
    //    InventoryItem beforeItem = selectedItem;

    //    if (selectedItem == null) { return; } // ���� �������� ���ٸ� ���� ����
    //    selectedItemGrid.OverlapCheck(selectedItem.onGridPositionX, selectedItem.onGridPositionY, selectedItem.HEIGHT, selectedItem.WIDTH, ref overlapItem);

    //    if (overlapItem != null)
    //    {
    //        print("��ħ");
    //        return;
    //    }
    //    else
    //    {
    //        selectedItem.Rotate(clockwise);
    //        print("�Ȱ�ħ");
    //    }

    //    PlaceItem(before);
    //    selectedItem = beforeItem;
    //    PickUpItem(before);

    //    MoveItem();
    //}

    //private void CreateRandomItem() // ���� ������ ���� �׽�Ʈ��
    //{
    //    InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
    //    selectedItem = inventoryItem; // ������ ������ ����

    //    rectTransform = inventoryItem.GetComponent<RectTransform>();
    //    rectTransform.SetParent(canvasTransform);

    //    int selectedItemID = UnityEngine.Random.Range(0, items.Count);
    //    inventoryItem.Set(items[selectedItemID]);
    //}

    //private void LeftMouseButtonPress()
    //{
    //    Vector2 position = Input.mousePosition;

    //    if (selectedItem != null) // �������� �����ٸ�
    //    {
    //        position.x -= (selectedItem.WIDTH - 1) * InventoryGrid.tileSizeWidth / 2;
    //        position.y += (selectedItem.HEIGHT - 1) * InventoryGrid.tileSizeHeight / 2;
    //    }

    //    Vector2Int tileGridPosition = selectedItemGrid.GetTileGridPosition(position); // ���콺 ��ġ �޾ƿ��� 

    //    print(tileGridPosition);

    //    if (selectedItem == null) // �������� �������ٸ�
    //    {
    //        PickUpItem(tileGridPosition); // ������ ����
    //    }
    //    else // �������� �����ٸ�
    //    {
    //        if (isMove)
    //        {
    //            PlaceItem(tileGridPosition); // ������ ��ġ ���ϱ�
    //        }
    //    }
    //}

    //private void PlaceItem(Vector2Int tileGridPosition) // ������ ��ġ �α�
    //{
    //    bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem); // ���� �������� �ξ��°�

    //    if (complete) // ���� �������� �״ٸ�
    //    {
    //        selectedItem = null; // ���� ������ ���ֱ�

    //        isMove = false;
    //        inventoryMoveImage.sprite = null;

    //        if (overlapItem != null) // �ߺ��� ��ġ�� �������� �ִٸ�
    //        {
    //            selectedItem = overlapItem; // �տ� ����
    //            overlapItem = null; // �ߺ��� ������ ���ֱ�
    //            rectTransform = selectedItem.GetComponent<RectTransform>(); // �̵� ��Ű���� transform �޾ƿ���

    //            // ������ �� ��ģ ������ selectâ ���� �̹��� ���� 
    //        }
    //    }

    //}

    //private void PickUpItem(Vector2Int tileGridPosition) // ������ ���� ���� �����ٸ� �̵��� ���� transform ��������
    //{
    //    selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
    //    if (selectedItem != null)
    //    {
    //        rectTransform = selectedItem.GetComponent<RectTransform>();
    //    }
    //}

    //private void SelectItem(InventoryItem select)
    //{
    //    if (selectedItem != null && isMove == false)
    //    {
    //        inventorySelectPanel.SetActive(true);
    //        selectedItem = select;
    //        inventorySelectImage.sprite = selectedItem.itemData.itemicon;
    //    }
    //}

    //private void LooseItem()
    //{
    //    inventorySelectPanel.SetActive(false);
    //    Destroy(selectedItem.gameObject);
    //}

    //private void MoveItem()
    //{
    //    isMove = true;
    //    inventorySelectPanel.SetActive(false);
    //    inventoryMoveImage.sprite = selectedItem.itemData.itemicon;

    //    // ���� ������ loose ��Ű��
    //}
}
