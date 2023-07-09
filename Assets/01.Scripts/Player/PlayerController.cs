using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ModuleController
{
    private PlayerActionData _actionData;
    public PlayerActionData ActionData => _actionData;
    
    [SerializeField]
    private PlayerDataSO _dataSO;
    public PlayerDataSO DataSO => _dataSO;

    private readonly Dictionary<PlayerState, Module> _playerModules = new Dictionary<PlayerState, Module>();

    protected override void Awake()
    {
        base.Awake();

        foreach(var module in _modules){
            if(module.GetType() == typeof(PlayableMovementModule)){
                _playerModules.Add(PlayerState.PLAYABLE, module);
            }
            else if(module.GetType() == typeof(ClickerMovementModule)){
                _playerModules.Add(PlayerState.CLICKER, module);
            }
        }

        _actionData = GetComponent<PlayerActionData>();
    }

    protected override void Update()
    {
        _playerModules[_actionData.PlayerState].UpdateModule();
    }

    protected override void FixedUpdate()
    {
        _playerModules[_actionData.PlayerState].FixedUpdateModule();
    }
}
