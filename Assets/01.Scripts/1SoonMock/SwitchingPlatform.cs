using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingPlatform : MonoBehaviour
{
    public enum PlatformType
    {
        A,
        B
    }
    public PlatformType thisType;

    public void SetState(bool state)
    {
        GetComponent<Collider>().enabled = state;
        GetComponent<MeshRenderer>().enabled = state;
    }
}
