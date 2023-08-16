using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryPopup : UIPopup
{
    private VisualElement _exitBtn;
    private VisualElement _rightRotateBtn;
    private VisualElement _leftRotateBtn;

    public InventoryUnit selectedUnit = null;

    [SerializeField]
    private VisualTreeAsset _unitTemplate;
    
    private VisualElement _unitParent;

    private InventoryTile[,] _tiles;
    private List<InventoryUnit> _units;

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

        _tiles = new InventoryTile[InventoryManager.BoardSizeY, InventoryManager.BoardSizeX];
        for(int i = 0; i < InventoryManager.BoardSizeY; i++)
        {
            for(int j = 0; j < InventoryManager.BoardSizeX; j++)
            {
                _tiles[i, j] = new InventoryTile
                {
                    xIdx = j,
                    yIdx = i
                };
            }
        }

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
        InventoryData data = (InventoryData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.InventoryData);
        _units = data.Units.List;
        foreach (InventoryUnit unit in _units)
        {
            VisualElement root = _unitTemplate.Instantiate();
            root = root.Q("inventory-unit");
            unit.Generate(root);
            _unitParent.Add(root);
        }
    }

    public bool Search(int minX, int maxX, int minY, int maxY)
    {
        if (selectedUnit == null)
            return false;
        
        if (selectedUnit.MinX < 0 
            || selectedUnit.MaxX > InventoryManager.BoardSizeX 
            || selectedUnit.MinY < 0 || selectedUnit.MaxY > InventoryManager.BoardSizeY)
            return false;
        
        foreach (var unit in _units)
        {
            if(unit != selectedUnit)
            {
                if ((minX >= unit.MinX || maxX <= unit.MaxX)
              && (minY >= unit.MinY || maxY <= unit.MaxY))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            InventoryData data = (InventoryData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.InventoryData);
            data.Units.List = _units;
            RemoveRoot();
        });

        _rightRotateBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (selectedUnit != null)
            {
                selectedUnit.rotate--;
                if (selectedUnit.rotate < 0) selectedUnit.rotate = 3;

                if (Search(selectedUnit.MinX, selectedUnit.MaxX, selectedUnit.MinY, selectedUnit.MaxY))
                {
                    Debug.Log(1);
                    selectedUnit.Rotate(-90f);
                }
                else
                {
                    if (selectedUnit.rotate + 1 > 3)
                        selectedUnit.rotate = 0;
                    else
                        selectedUnit.rotate++;
                }
            }
        });

        _leftRotateBtn.RegisterCallback<ClickEvent>(e =>
        {
            if(selectedUnit != null)
            {
                selectedUnit.rotate++;
                if (selectedUnit.rotate > 3) selectedUnit.rotate = 0;

                if (Search(selectedUnit.MinX, selectedUnit.MaxX, selectedUnit.MinY, selectedUnit.MaxY))
                {
                    Debug.Log(1);
                    selectedUnit.Rotate(90f);
                }
                else
                {
                    if (selectedUnit.rotate - 1 < 0)
                        selectedUnit.rotate = 3;
                    else 
                        selectedUnit.rotate--;
                }
            }
        });

        for(int i = 0; i < InventoryManager.BoardSizeY; i++)
        {
            for(int j = 0; j < InventoryManager.BoardSizeX; j++)
            {
                int i1 = i;
                int j1 = j;

                _tiles[i, j].tile.RegisterCallback<ClickEvent>(e =>
                {
                    if (selectedUnit != null)
                    {
                        Debug.Log(selectedUnit);
                        if (Search(selectedUnit.MinX, selectedUnit.MaxX, selectedUnit.MinY, selectedUnit.MaxY))
                        {
                            Debug.Log("움직임");
                            selectedUnit.Move(new Vector2(_tiles[i1, j1].xIdx, _tiles[i1, j1].yIdx));
                        }
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

        _unitParent = _root.Q<VisualElement>("contents");

        List<VisualElement> tiles = _root.Query<VisualElement>(className: "inventory-unit").ToList();
        int k = 0;
        for(int i = 0; i < InventoryManager.BoardSizeX; i++)
        {
            for(int j = 0; j < InventoryManager.BoardSizeY; j++)
            {
                _tiles[j, i].tile = tiles[k];
                k++;
            }
        }
    }
}