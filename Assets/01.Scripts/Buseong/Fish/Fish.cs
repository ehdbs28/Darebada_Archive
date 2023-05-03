using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private BIOME _habitatBiome;
    private float _habitatX; //서식지X값
    private float _habitatY; //서식지Y값
    private FISHSPECIES _fishSpecies;
    private float _swimSpeed; //헤엄 속도
    public float SwimSpeed => _swimSpeed;
    private float _turnSpeed; //회전 속도
    private float _rarity;    
    private float _level;
    private float _cost;
    private float _size;
    private float _spawnPercent;

    public void Init(FishSO data)
    {
        _habitatBiome = data.HabitatBiome;
        _habitatX = data.HabitatX;
        _habitatY = data.HabitatY;
        _fishSpecies = data.FishSpecies;
        _swimSpeed = data.SwimSpeed;
        _turnSpeed = data.TurnSpeed;
        _rarity = data.Rarity;
        _level = data.Level;
        _cost = data.Cost;
        _size = data.Size;
        _spawnPercent = data.SpawnPercent;
    }
}
