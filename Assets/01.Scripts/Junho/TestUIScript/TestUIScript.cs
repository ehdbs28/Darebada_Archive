using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestUIScript : MonoBehaviour
{
    public void OnSceneChange(string a)
    {
        SceneManager.LoadScene(a);
    }
}
