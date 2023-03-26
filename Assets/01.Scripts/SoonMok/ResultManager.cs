using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    private void Awake()
    {
         ZIpperResult[] result = GameObject.FindObjectsOfType<ZIpperResult>();
        foreach(ZIpperResult sc in result)
        {
            switch(sc.id)
            {
                //����Ʈ���� �� �ൿ ����.
                case 0:
                    {
                        sc.resultAction =()=> {
                            Debug.Log("§��");
                        };
                    }
                    break;
                    case 1:
                    {
                        sc.instantiateAction = (GameObject a) =>
                        {
                            GameObject obj = Instantiate(a);
                            a.transform.position = sc.instantiatePos;
                        };
                    }
                    break;
                default: break;
            }
        }

    }
}
