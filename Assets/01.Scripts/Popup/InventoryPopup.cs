using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryPopup : UIPopup
{
    private VisualElement _exitBtn;
    private VisualElement _rightRotateBtn;
    private VisualElement _leftRotateBtn;

    public InventoryUnit _selectedUnit;
    public VisualElement _selectedUnitProfile;

    [SerializeField]
    private VisualTreeAsset _unitTemplate;
    public VisualTreeAsset UnitTemplate
    {
        get => _unitTemplate;
    }
    private VisualElement _unitParent;
    public VisualElement UnitParent
    {
        get => UnitParent;
    }

    private List<InventoryUnit> _units;
    private InventoryTile[,] _tiles;

    private FishDataTable dataTable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            InventoryUnit newUnit = new InventoryUnit(_unitTemplate, _unitParent, dataTable.DataTable[0]);
            newUnit.PosX = 3;
            newUnit.PosY = 0;
            newUnit.Rotate = 0;
            newUnit.Generate(newUnit);
            _units.Add(newUnit);

            InventoryUnit newUnit2 = new InventoryUnit(_unitTemplate, _unitParent, dataTable.DataTable[0]);
            newUnit2.PosX = 3;
            newUnit2.PosY = 5;
            newUnit2.Rotate = 0;
            newUnit2.Generate(newUnit2);
            _units.Add(newUnit2);
        }
    }

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        _isOpenPopup = true;

        _documentRoot = document.rootVisualElement.Q("main-container");

        if (clearScreen && _documentRoot.childCount >= 2)
        {
            for (int i = 0; i < _documentRoot.childCount; i++)
            {
                if (_documentRoot.ElementAt(i).ClassListContains("blur-panel")) continue;
                _documentRoot.RemoveAt(i);
            }
        }

        if (timeStop)
            GameManager.Instance.GetManager<TimeManager>().TimeScale = 0f;

        if (blur)
        {
            _blurPanel = _documentRoot.Q(className: "blur-panel");
            _blurPanel.AddToClassList("on");
        }

        dataTable = (FishDataTable)GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.FishData);
        _units = new List<InventoryUnit>();
        _tiles = new InventoryTile[InventoryManager.Instance.Board_Size_Y, InventoryManager.Instance.Board_Size_X];
        for(int i = 0; i < InventoryManager.Instance.Board_Size_Y; i++)
        {
            for(int j = 0; j < InventoryManager.Instance.Board_Size_X; j++)
            {
                _tiles[i, j] = new InventoryTile();
                _tiles[i, j].xIdx = j;
                _tiles[i, j].yIdx = i;
            }
        }
        //_tiles = InventoryManager.Instance.Tiles;

        GenerateRoot();
        GenerateInventoryUnit();

        if (_root != null)
        {
            AddEvent();
            _documentRoot.Add(_root);
        }
    }

    private void GenerateInventoryUnit()
    {
        List<InventoryUnit> inventoryUnits = InventoryManager.Instance.Units;
        for (int i = 0; i < inventoryUnits.Count; i++)
        {
            //여기서 필요한 데이터를 데이터 테이블에서 넘겨주도록 해야함
            InventoryUnit newUnit = new InventoryUnit(_unitTemplate, _unitParent, inventoryUnits[i].Data);
            newUnit.Generate(newUnit);
            _units.Add(newUnit);
        }
    }

    public bool Search(int minX, int maxX, int minY, int maxY)
    {
        if (_selectedUnit.GetMinX() < 0 
            || _selectedUnit.GetMaxX() > InventoryManager.Instance.Board_Size_X 
            || _selectedUnit.GetMinY() < 0 || _selectedUnit.GetMaxY() > InventoryManager.Instance.Board_Size_Y)
            return false;
        foreach (var unit in _units)
        {
            if(unit != _selectedUnit)
            {
                if ((minX >= unit.GetMinX() || maxX <= unit.GetMaxX())
              && (minY >= unit.GetMinY() || maxY <= unit.GetMaxY()))
                {
                    return false;
                }
            }
        }

        return true;

        ////나중에 GameManager통해서 InventoryManager에 접근하도록 수정해야함.
        //bool[,] temp = new bool[InventoryManager.Instance.Board_Size_Y, InventoryManager.Instance.Board_Size_X];
        //int curSize = 0;

        //for(int i = 0; i < InventoryManager.Instance.Board_Size_Y; i++)
        //{
        //    for(int j = 0; j < InventoryManager.Instance.Board_Size_X; j++)
        //    {
        //        temp[i, j] = tiles[i, j].IsFull;
        //    }
        //}

        //Queue<Vector2> q = new Queue<Vector2>();
        //q.Enqueue(new Vector2(start_x, start_y));
        //temp[start_y, start_x] = true;

        //while(q.Count > 0)
        //{
        //    Vector2 node = q.Peek();
        //    q.Dequeue();

        //    for(int i = 0; i < 4; i++)
        //    {
        //        curSize++;
        //        int nextX = (int)node.x + InventoryManager.Instance.destX[i];
        //        int nextY = (int)node.y + InventoryManager.Instance.destY[i];

        //        if (nextX < minX || nextX > maxX || nextY < minY || nextY > maxY) break;
        //        if (temp[nextY, nextX] == true) break;

        //        temp[nextY, nextX] = true;
        //        q.Enqueue(new Vector2(nextX, nextY));
        //    }
        //}

        //return (curSize == size ? true : false);
    }

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            InventoryManager.Instance.Units = _units;
            InventoryManager.Instance.Tiles = _tiles;
            RemoveRoot();
        });

        _rightRotateBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (_selectedUnit != null)
            {
                //_selectedUnit.Rotate = Mathf.Abs((_selectedUnit.Rotate - 1) % 3);
                _selectedUnit.Rotate--;
                if (_selectedUnit.Rotate < 0) _selectedUnit.Rotate = 3;

                if (Search(_selectedUnit.GetMinX(), _selectedUnit.GetMaxX(), _selectedUnit.GetMinY(), _selectedUnit.GetMaxY()))
                {
                    _selectedUnit.Root.style.rotate = new Rotate(_selectedUnit.Root.style.rotate.value.angle.value - 90);
                }
                else
                {
                    Debug.Log(_selectedUnit.Rotate);
                    _selectedUnit.Rotate++;
                }
            }
        });

        _leftRotateBtn.RegisterCallback<ClickEvent>(e =>
        {
            if(_selectedUnit != null)
            {
                _selectedUnit.Rotate++;
                if (_selectedUnit.Rotate > 3) _selectedUnit.Rotate = 0;

                if (Search(_selectedUnit.GetMinX(), _selectedUnit.GetMaxX(), _selectedUnit.GetMinY(), _selectedUnit.GetMaxY()))
                {
                    _selectedUnit.Root.style.rotate = new Rotate(_selectedUnit.Root.style.rotate.value.angle.value + 90);
                }
                else
                {
                    Debug.Log(_selectedUnit.Rotate);
                    if (_selectedUnit.Rotate < 0) _selectedUnit.Rotate = 3;
                    else _selectedUnit.Rotate--;
                }
            }
        });

        for(int i = 0; i < InventoryManager.Instance.Board_Size_Y; i++)
        {
            for(int j = 0; j < InventoryManager.Instance.Board_Size_X; j++)
            {
                //Debug.Log($"xIdx: {_tiles[i, j].xIdx}");
                //Debug.Log($"yIdx: {_tiles[i, j].yIdx}");

                int _i = i;
                int _j = j;

                _tiles[i, j].tile.RegisterCallback<ClickEvent>(e =>
                {
                    
                    Debug.Log(_tiles[_i, _j]);
                    InventoryUnit temp = _selectedUnit;
                    //_selectedUnit.PosX = j;
                    //_selectedUnit.PosY = i;
                    _selectedUnit.PosY = _tiles[_i, _j].yIdx;
                    _selectedUnit.PosX = _tiles[_i, _j].xIdx;
                    if (_selectedUnit != null && Search(_selectedUnit.GetMinX(), _selectedUnit.GetMaxX(), _selectedUnit.GetMinY(), _selectedUnit.GetMaxY()))
                    {
                        Debug.Log("움직임");
                        _selectedUnit.Root.style.left = _selectedUnit.PosX * 100 + 10;
                        _selectedUnit.Root.style.top = _selectedUnit.PosY * 100 + 10;
                    }
                    else
                    {
                        Debug.Log("안 움직임");
                        _selectedUnit.PosX = temp.PosX;
                        _selectedUnit.PosY = temp.PosY;
                    }
                });
            }
        }
    }

    public override void RemoveEvent()
    {
    }

    public override void FindElement()
    {
        _exitBtn = _root.Q<VisualElement>("exit-btn");
        _rightRotateBtn = _root.Q<VisualElement>("rotate-right-btn");
        _leftRotateBtn = _root.Q<VisualElement>("rotate-left-btn");
        _selectedUnitProfile = _root.Q<VisualElement>("inner");

        _unitParent = _root.Q<VisualElement>("contents");

        List<VisualElement> tiles = _root.Query<VisualElement>(className: "inventory-unit").ToList();
        int k = 0;
        for(int i = 0; i < InventoryManager.Instance.Board_Size_X; i++)
        {
            for(int j = 0; j < InventoryManager.Instance.Board_Size_Y; j++)
            {
                _tiles[j, i].tile = tiles[k];
                k++;
            }
        }
    }
}