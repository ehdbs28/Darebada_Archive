using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelect : MonoBehaviour
{
    [SerializeField] private Image _inventoryMove;

    [SerializeField] private Button _moveButton;
    [SerializeField] private Button _looseButton;

    private void Start()
    {
        _moveButton.onClick.AddListener(MoveFish);
        _looseButton.onClick.AddListener(LooseFish);
    }

    private void LooseFish() // 방생 버튼에 연결하기
    {
        print($"{InventoryManager.Instance._item.name} 방생");
        InventoryManager.Instance._item.GetComponent<Image>().sprite = null;
        gameObject.SetActive(false);
    }

    private void MoveFish() // 이동 버튼에 연결하기
    {
        _inventoryMove.sprite = InventoryManager.Instance._item.GetComponent<Image>().sprite;
        InventoryManager.Instance._isChange = true;
        gameObject.SetActive(false);
    }
}
