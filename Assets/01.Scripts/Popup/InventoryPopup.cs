using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryPopup : UIPopup
{
    private VisualElement _exitBtn;
    private VisualElement _rightRotateBtn;
    private VisualElement _leftRotateBtn;

    [SerializeField]
    private VisualTreeAsset _unitTemplate;
    
    private VisualElement _unitParent;

    private InventoryTile[,] _tiles;
    [SerializeField]
    private List<InventoryUnit> _units;

    public int selectedIndex = -1;
    
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

    public override void RemoveRoot()
    {
        selectedIndex = -1;
        InventoryData data = (InventoryData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.InventoryData);
        data.Units.List = _units;
        base.RemoveRoot();
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
            //유닛 위치로직
            unit.Move(new Vector2(unit.posX, unit.posY));
        }
    }

    public bool Search(int minX, int maxX, int minY, int maxY)
    {
        if (selectedIndex == -1)
            return false;

        if (_units[selectedIndex].MinX < 0 
            || _units[selectedIndex].MaxX > InventoryManager.BoardSizeX 
            || _units[selectedIndex].MinY < 0 || _units[selectedIndex].MaxY > InventoryManager.BoardSizeY)
            return false;
        
        foreach (var unit in _units)
        {
            if(unit != _units[selectedIndex])
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
            RemoveRoot();
        });

        _rightRotateBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (selectedIndex == -1)
                return;
            
            if (_units[selectedIndex] != null)
            {
                _units[selectedIndex].rotate--;
                if (_units[selectedIndex].rotate < 0) _units[selectedIndex].rotate = 3;

                if (Search(_units[selectedIndex].MinX, _units[selectedIndex].MaxX, _units[selectedIndex].MinY, _units[selectedIndex].MaxY))
                {
                    _units[selectedIndex].Rotate(-90f);
                }
                else
                {
                    if (_units[selectedIndex].rotate + 1 > 3)
                        _units[selectedIndex].rotate = 0;
                    else
                        _units[selectedIndex].rotate++;
                }
            }
        });

        _leftRotateBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (selectedIndex == -1)
                return;
            
            if(_units[selectedIndex] != null)
            {
                _units[selectedIndex].rotate++;
                if (_units[selectedIndex].rotate > 3) _units[selectedIndex].rotate = 0;

                if (Search(_units[selectedIndex].MinX, _units[selectedIndex].MaxX, _units[selectedIndex].MinY, _units[selectedIndex].MaxY))
                {
                    _units[selectedIndex].Rotate(90f);
                }
                else
                {
                    if (_units[selectedIndex].rotate - 1 < 0)
                        _units[selectedIndex].rotate = 3;
                    else 
                        _units[selectedIndex].rotate--;
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
                    if (selectedIndex == -1)
                        return;

                    if (_units[selectedIndex] != null)
                    {
                        if (Search(_units[selectedIndex].MinX, _units[selectedIndex].MaxX, _units[selectedIndex].MinY, _units[selectedIndex].MaxY))
                        {
                            _units[selectedIndex].Move(new Vector2(j1, i1));
                        }
                    }
                });
            }
        }

        for (int i = 0; i < _units.Count; i++)
        {
            int i1 = i;
            
            _units[i].Root.RegisterCallback<ClickEvent>(e =>
            {
                selectedIndex = i1;
            });
        }
    }

    public override void RemoveEvent(){}

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