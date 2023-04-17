using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 OpenVec;

    private void OpenDoor()
    {
        gameObject.transform.position = OpenVec;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            OpenDoor();
            Destroy(collision.gameObject);
        } 

    }
}
