using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoonMockFish : MonoBehaviour
{
    
    public string fishName;
    public int id;
    public int amount;
    [SerializeField] private SFISHSO fishSO;
    public void ChangeName()
    {
        fishName = fishSO.names[id];
    }
}
