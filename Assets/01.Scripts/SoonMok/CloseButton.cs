using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public void CloseWindow()
    {
        Destroy(transform.parent.gameObject);
    }
}
