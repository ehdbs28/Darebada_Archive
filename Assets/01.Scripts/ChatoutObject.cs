using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatoutObject : MonoBehaviour
{
    [SerializeField] private GameObject alphabet;
    string a;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        { 
            a = gameObject.GetComponent<TMP_InputField>().text;

            if (a.Length > 1)
                gameObject.GetComponent<TMP_InputField>().text = "�ٽ� �Է��ϼ���!";
            else if (a.Length == 1)
                Instantiate(alphabet, Vector3.zero, Quaternion.identity);
        }
    }
}
