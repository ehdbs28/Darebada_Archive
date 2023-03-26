using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour, IsClickObj
{


    public void CloseWindow()
    {
        Destroy(transform.parent.gameObject);
    }

    public void OnClick()
    {
        CloseWindow();
    }

    public void OnDrag()
    {
        CloseWindow();
    }

    public void OnDragEnd()
    {
        CloseWindow();
    }
}
