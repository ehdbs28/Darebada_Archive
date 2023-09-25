using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class UISelectItemUnit : UIInteractionElement
{
    private ShopItemUnit _unit;
    
    private VisualElement _imageBox;
    private Label _nameLabel;
    private Label _descLabel;
    private Label _countLable;

    private FishingItemType _type;
    private bool _isEquip = false;

    private StringBuilder _countSb;

    private FishingData _data;
    private List<UISelectItemUnit> _units;

    public UISelectItemUnit(VisualElement root, ShopItemUnit unit, List<UISelectItemUnit> units) : base(root)
    {
        _countSb = new StringBuilder();
        _unit = unit;
        _units = units;
        
        FindElement();
        Setting();
        AddEvent();
    }

    private void UpdateUnitState()
    {
        _data = (FishingData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData);

        int count = _data.ItemVal[_unit.Index];
        
        _countSb.Clear();
        _countSb.Append(count.ToString());
        _countSb.Append("개 보유중");

        _countLable.text = _countSb.ToString();
        
        if(_data.CurSelectedItem == (FishingItemType)_unit.Index)
            _root.AddToClassList("equip");
        else
            _root.RemoveFromClassList("equip");
        
        if(count == 0)
            _root.AddToClassList("empty");
        else
            _root.RemoveFromClassList("empty");
    }

    private void Setting()
    {
        UpdateUnitState();
        _imageBox.style.backgroundImage = new StyleBackground(_unit.Image);
        _nameLabel.text = _unit.Name;
        _descLabel.text = _unit.Desc;
    }

    protected override void AddEvent()
    {
        _interactionBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (_data.CurSelectedItem == (FishingItemType)_unit.Index)
                return;
            
            if (GameManager.Instance.GetManager<SeleteItemManager>().EquipItem((FishingItemType)_unit.Index))
            {
                PlayParticle();
                foreach (var unit in _units)
                {
                    unit.UpdateUnitState();
                }
            }
        });
    }

    private void PlayParticle()
    {
        Vector2 btnPos = GameManager.Instance.GetManager<UIManager>()
            .GetElementPos(_interactionBtn, new Vector2(0.5f, 0.5f));
        
        PoolableUIParticle particle = GameManager.Instance.GetManager<PoolManager>().Pop("TadaEffect") as PoolableUIParticle;
        particle.SetPoint(btnPos);
        particle.Play();
    }

    protected override void FindElement()
    {
        base.FindElement();

        _imageBox = _root.Q<VisualElement>("item-image");
        _nameLabel = _root.Q<Label>("item-name");
        _descLabel = _root.Q<Label>("item-desc");
        _countLable = _root.Q<Label>("count-text");
    }
}
