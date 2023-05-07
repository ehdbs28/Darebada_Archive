using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BiomeIcon : MonoBehaviour
{
    public static bool alreadyEntered;
    [SerializeField] string bieomeName;
    public string BiomeName => bieomeName;
    private void OnCollisionEnter(Collision collision)
    {
        if(!alreadyEntered)
        {
            StartCoroutine(ChangeScene());
            alreadyEntered = true;
            
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(bieomeName);
    }
}
