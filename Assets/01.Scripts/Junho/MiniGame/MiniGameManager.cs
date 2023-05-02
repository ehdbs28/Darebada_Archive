using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    public List<GameObject> hitPoint;
    public GameObject roundArea;
    public float radius;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multy MiniGameManager");
        }
        Instance = this;
    }

    public void PointRandomSpawn()
    {
        Vector3 retVector = roundArea.transform.position;
        float degree = Random.Range(0, 361);
        float radian = degree * Mathf.PI / 180;

        retVector.x += radius * Mathf.Cos(radian) * 60;
        retVector.y += radius * Mathf.Sin(radian) * 60;

        hitPoint[0].transform.position = retVector;
    }
}
