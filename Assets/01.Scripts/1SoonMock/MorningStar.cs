using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class MorningStar : MonoBehaviour
{
    public float angle;
    public float PosAndNeg;
    public bool trapActive;
    public float speed;
    public float x, z;
    private void Awake()
    {
        Vector3 dir = new Vector3(x, 0, z).normalized;
        StartCoroutine(Turning(dir));
    }
    IEnumerator Turning(Vector3 dir)
    {
        while(true)
        {
            if(trapActive)
            {
                transform.eulerAngles += dir * PosAndNeg * Time.deltaTime * speed;
                if (isOvered())
                {
                    PosAndNeg *= -1;
                }
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    bool isOvered()
    {
        Vector3 Rot = transform.localEulerAngles;
        if ((Rot.x > x * angle && Rot.x < 360 + x * -angle) && x != 0)
        {
            Debug.Log(Rot.x);
            return true;
        }
        if ((Rot.z > z * angle && Rot.z < 360 + z * -angle) && z!= 0)
        {
            Debug.Log(Rot.y);
            return true;
        }
            
        return false;
    }
}
