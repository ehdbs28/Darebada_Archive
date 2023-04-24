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
                name = "참돔";
                break;
            case 1:
                name = "황천의 오징어";
                break;
            case 2:
                name = "천-년동안-응-어리진-내-분노를-느껴라-";
                break;
            case 3:
                name = "날아다니는 날치 괴물";
                break;
            case 4:
                name = "참돔";
                break;
            default:
                name = "쑥";
                break;
        }
    }
}
