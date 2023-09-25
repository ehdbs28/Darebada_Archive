using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BiomeIcon : MonoBehaviour
{
    public static bool alreadyEntered;
    [SerializeField] BIOME _bieome;

    public BIOME BiomeName => _bieome;
    private void OnCollisionEnter(Collision collision)
    {
        if(!alreadyEntered)
        {
            StartCoroutine(ChangeScene(collision.gameObject.GetComponent<BoatInput>().item));
            alreadyEntered = true;
        }
    }
    IEnumerator ChangeScene(ItemSO item)
    {
        yield return new WaitForSeconds(1f);
        item.HabitatBiome = _bieome;
    }
}
