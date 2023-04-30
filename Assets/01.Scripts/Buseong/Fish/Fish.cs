using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private BIOME _habitatBiome;
    private float _habitatX; //무브먼트에서
    private float _habitatY; //무브먼트에서
    private FISHSPECIES _fishSpecies;
    private Vector3 _direction; //무브먼트에서
    private float _swimSpeed; //무브먼트에서
    private float _turnSpeed; //무브먼트에서
    private float _rarity;    
    private float _level;
    private float _cost;
    private float _size;
    private float _spawnPercent;
    public GameObject _fishObject;

    public Fish(FishSO data)
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
        _fishObject = Instantiate(data._fishPrefab, new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)), Quaternion.identity);
        _fishObject.GetComponent<MeshRenderer>().material = data.TestMat;
        //Instantiate(data._fishPrefab, new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)), Quaternion.identity);
    }
}
