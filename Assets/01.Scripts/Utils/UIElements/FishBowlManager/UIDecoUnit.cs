using UnityEngine.UIElements;

public class UIDecoUnit
{
    private VisualElement _root;
    private VisualElement _checkElem;
    
    private Fishbowl _fishbowl;

    private bool _isEquip;

    private int _idx;
    
    public UIDecoUnit(VisualElement root, Fishbowl fishbowl, int idx)
    {
        _root = root;
        _fishbowl = fishbowl;
        _idx = idx;
        
        _isEquip = fishbowl.AlreadyHadDeco(idx);
        
        FindElement();
        AddEvent();
    }

    private void FindElement()
    {
        _checkElem = _root.Q("check");
    }

    private void AddEvent()
    {
        _root.RegisterCallback<ClickEvent>(e =>
        {
            if (!_isEquip)
            {
                GameManager.Instance.GetManager<MoneyManager>().Payment(25, () =>
                {
                    _isEquip = true;
                    _checkElem.style.opacity = 1f;
                    _fishbowl.AddDeco(_idx);
                });
            }
            else
            {
                _isEquip = false;
                _checkElem.style.opacity = 0f;
                _fishbowl.RemoveDeco(_idx);
            }
        });
    }
}
