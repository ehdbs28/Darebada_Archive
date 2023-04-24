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

    private void LooseFish() // ��� ��ư�� �����ϱ�
    {
        print($"{InventoryManager.Instance._item.name} ���");
        InventoryManager.Instance._item.GetComponent<Image>().sprite = null;
        gameObject.SetActive(false);
    }

    private void MoveFish() // �̵� ��ư�� �����ϱ�
    {
        _inventoryMove.sprite = InventoryManager.Instance._item.GetComponent<Image>().sprite;
        InventoryManager.Instance._isChange = true;
        gameObject.SetActive(false);
    }
}
