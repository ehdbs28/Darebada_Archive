using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public void OnDrived()
    {
        gameObject.SetActive(false);
    }
}
