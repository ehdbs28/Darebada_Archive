using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelect_Proto : MonoBehaviour
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
        print($"{InventoryManager_Proto.Instance._item.name} 방생");
        InventoryManager_Proto.Instance._item.GetComponent<Image>().sprite = null;
        gameObject.SetActive(false);
    }

    private void MoveFish() // 이동 버튼에 연결하기
    {
        _inventoryMove.sprite = InventoryManager_Proto.Instance._item.GetComponent<Image>().sprite;
        InventoryManager_Proto.Instance._isChange = true;
        gameObject.SetActive(false);
    }
}
