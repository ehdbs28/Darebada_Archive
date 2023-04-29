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

    private void LooseFish() // ��� ��ư�� �����ϱ�
    {
        print($"{InventoryManager_Proto.Instance._item.name} ���");
        InventoryManager_Proto.Instance._item.GetComponent<Image>().sprite = null;
        gameObject.SetActive(false);
    }

    private void MoveFish() // �̵� ��ư�� �����ϱ�
    {
        _inventoryMove.sprite = InventoryManager_Proto.Instance._item.GetComponent<Image>().sprite;
        InventoryManager_Proto.Instance._isChange = true;
        gameObject.SetActive(false);
    }
}
