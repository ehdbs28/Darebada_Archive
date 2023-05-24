using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitZone : MonoBehaviour
{
    public float requireLevel;
    public float durablityDamageAmount;
    public BoatActionData boat;
    private void Awake()
    {
        boat = FindObjectOfType<BoatActionData>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<BoatActionData>())
        {
            boat.CurrentDurability -= Time.deltaTime * durablityDamageAmount;
        }
    }
}
