using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIoutObject : MonoBehaviour
{
    [SerializeField] private GameObject backObj;

    private Button thisBtn;

    private void Awake()
    {
        thisBtn = gameObject.GetComponent<Button>(); 
    }

    private void Update()
    {
        thisBtn.onClick.AddListener(UItoGameObj);
    }

    private void UItoGameObj()
    {
        gameObject.SetActive(false);
        backObj.GetComponent<Rigidbody>().useGravity = true;
    }
}
