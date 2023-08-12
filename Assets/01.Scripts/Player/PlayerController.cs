using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ModuleController
{
    [SerializeField]
    private PlayerDataSO _dataSO;
    public PlayerDataSO DataSO => _dataSO;
}
