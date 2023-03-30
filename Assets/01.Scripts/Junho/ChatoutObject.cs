using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatoutObject : MonoBehaviour
{
    public List<GameObject> createObj;

    [SerializeField] private GameObject alphabet;
    string a;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        { 
            a = gameObject.GetComponent<TMP_InputField>().text;

            if (a.Length > 1)
                gameObject.GetComponent<TMP_InputField>().text = "다시 입력하세요!";
            else if (a.Length == 1)
            {
                if (createObj.Count > 19)
                {
                    Destroy(createObj[0].gameObject);
                    createObj.RemoveAt(0);
                    createObj.Add(Instantiate(alphabet, Vector3.zero, Quaternion.identity));
                }
                else
                {
                    createObj.Add(Instantiate(alphabet, Vector3.zero, Quaternion.identity));
                }

            }
        }
    }
}
