using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryPopup : UIPopup
{
    private VisualElement _exitBtn;

    private VisualElement _selectedUnitImage;
    private VisualElement _rightRotateBtn;
    private VisualElement _leftRotateBtn;

    [SerializeField]
    private VisualTreeAsset _unitTemplate;
    
    private VisualElement _unitParent;

    private InventoryTile[,] _tiles;
    
    private InventoryData _data;

    public int selectedIndex = -1;
    
    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        _isOpenPopup = true;

        _documentRoot = document.rootVisualElement.Q("main-container");

        if(clearScreen && _documentRoot.childCount >= 3){
            for(int i = 0; i < _documentRoot.childCount; i++)
            {
                if(_documentRoot.ElementAt(i).ClassListContains("blur-panel") || _documentRoot.ElementAt(i).ClassListContains("notification")) 
                    continue;
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
        base.RemoveRoot();
    }

    private void GenerateInventoryUnit()
    {
        _data = (InventoryData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.InventoryData);
        
        foreach (var unit in _data.Units.List)
        {
            VisualElement root = _unitTemplate.Instantiate();
            root = root.Q("inventory-unit");
            unit.Generate(root);
            _unitParent.Add(root);
        }
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
            
            if (_data.Units.List[selectedIndex] != null)
            {
                _data.Units.List[selectedIndex].rotate--;
                if (_data.Units.List[selectedIndex].rotate < 0) _data.Units.List[selectedIndex].rotate = 3;

                if (GameManager.Instance.GetManager<InventoryManager>().Search(_data.Units.List[selectedIndex], _data.Units.List[selectedIndex].MinX, _data.Units.List[selectedIndex].MaxX, _data.Units.List[selectedIndex].MinY, _data.Units.List[selectedIndex].MaxY))
                {
                    _data.Units.List[selectedIndex].Rotate(-90f);
                }
                else
                {
                    if (_data.Units.List[selectedIndex].rotate + 1 > 3)
                        _data.Units.List[selectedIndex].rotate = 0;
                    else
                        _data.Units.List[selectedIndex].rotate++;
                }
            }
        });

        _leftRotateBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (selectedIndex == -1)
                return;
            
            if(_data.Units.List[selectedIndex] != null)
            {
                _data.Units.List[selectedIndex].rotate++;
                if (_data.Units.List[selectedIndex].rotate > 3) _data.Units.List[selectedIndex].rotate = 0;

                if (GameManager.Instance.GetManager<InventoryManager>().Search(_data.Units.List[selectedIndex], _data.Units.List[selectedIndex].MinX, _data.Units.List[selectedIndex].MaxX, _data.Units.List[selectedIndex].MinY, _data.Units.List[selectedIndex].MaxY))
                {
                    _data.Units.List[selectedIndex].Rotate(90f);
                }
                else
                {
                    if (_data.Units.List[selectedIndex].rotate - 1 < 0)
                        _data.Units.List[selectedIndex].rotate = 3;
                    else 
                        _data.Units.List[selectedIndex].rotate--;
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

                    if (_data.Units.List[selectedIndex] != null)
                    {
                        Vector2 prevPos = new Vector2(_data.Units.List[selectedIndex].posX, _data.Units.List[selectedIndex].posY);
                        _data.Units.List[selectedIndex].Move(new Vector2(_tiles[i1, j1].xIdx, _tiles[i1, j1].yIdx));
                        
                        if (!GameManager.Instance.GetManager<InventoryManager>().Search(_data.Units.List[selectedIndex], _data.Units.List[selectedIndex].MinX, _data.Units.List[selectedIndex].MaxX, _data.Units.List[selectedIndex].MinY, _data.Units.List[selectedIndex].MaxY))
                        {
                            _data.Units.List[selectedIndex].Move(prevPos);
                        }
                    }
                });
            }
        }

        for (int i = 0; i < _data.Units.List.Count; i++)
        {
            int i1 = i;
            
            _data.Units.List[i].Root.RegisterCallback<ClickEvent>(e =>
            {
                if (selectedIndex != -1)
                {
                    _data.Units.List[selectedIndex].Selected(false);
                }
                
                selectedIndex = i1;
                _data.Units.List[i1].Selected(true);
                _selectedUnitImage.style.backgroundImage = new StyleBackground(_data.Units.List[i1].data.Visual.Profile);
            });
        }
    }

    public override void RemoveEvent(){}

    public override void FindElement()
    {
        _exitBtn = _root.Q<VisualElement>("exit-btn");

        _selectedUnitImage = _root.Q<VisualElement>("selected-object").Q("inner");
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