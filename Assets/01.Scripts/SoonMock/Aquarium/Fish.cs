using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public string name;
    public int id;
    public int amount;
    public void ChangeName()
    {
        switch(id)
        {
            case 0:
                name = "����";
                break;
            case 1:
                name = "Ȳõ�� ��¡��";
                break;
            case 2:
                name = "õ-�⵿��-��-���-��-�г븦-������-";
                break;
            case 3:
                name = "���ƴٴϴ� ��ġ ����";
                break;
            case 4:
                name = "����";
                break;
            default:
                name = "��";
                break;
        }
    }
}
