using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reef : MonoBehaviour
{
    public float fuelDamage;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<BoatActionData>())
        {
            collision.gameObject.GetComponent<BoatActionData>().CurrentDurability -= fuelDamage;
        }
    }
}
