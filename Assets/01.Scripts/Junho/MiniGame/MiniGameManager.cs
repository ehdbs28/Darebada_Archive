using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    public List<GameObject> hitPoint;
    public GameObject roundArea;
    public float radius;

    [SerializeField]
    private BaitSO _currentBait;
    public Vector3 HitPointIncreaseValue;
    private BiomeIcon _biomeIcon;
    private Fish _catchedFish;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multy MiniGameManager");
        }
        Instance = this;
    }

    public float SetRange()
    {
        //HitPointIncreaseValue
        //_currentBait.VaildBIOME = ;

        /*if (_currentBait.VaildBiome.ToString() == _biomeIcon.BiomeName)
        {
            HitPointIncreaseValue.y += _currentBait.VaildBiomeIncreaseValue;
        }
        if (_currentBait.VaildMinTime < _currentTime && _currentBait.VaildMaxTime > _currentTime)
        {
            HitPointIncreaseValue.y += _currentBait.VaildTimeIncreaseValue;
        }
        if (_currentBait.VaildMinDepth < _currentDepth && _currentBait.VaildMaxDepth > _currentDepth)
        {
            radius -= _currentBait.VaildDepthIncreaseValue;
        }
        if (_currentBait.VaildRarity == _catchedFish.)
        {
            radius -= _currentBait.VaildRarityIncreaseValue;
        }*/

        for (int i = 0; i < hitPoint.Count; i++)
        {
            hitPoint[i].transform.localScale += HitPointIncreaseValue;
        }

        return HitPointIncreaseValue.y;
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
