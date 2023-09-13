using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject gridObject;
    public List<GridCell> grids;
    public float width;
    public float height;
    [SerializeField] float _distance;
    public void HideGrid()
    {
        for(int i = 0; i <  grids.Count; i++)
        {
            grids[i].gameObject.SetActive(false);
        }
    }
    private void CreateGrids()
    {
        //Vector3 floorSize = GameManager.Instance.GetManager<AquariumManager>().FloorSize;
        Vector3 floorSize = GameManager.Instance.GetManager<AquariumNumericalManager>().FloorSize;

        width = floorSize.x * 4;
        height = floorSize.z * 4;
        if (width * height >= grids.Count)
        {
            while(width * height > grids.Count)
            {
                GameObject grid = Instantiate(gridObject, transform);
                grids.Add(grid.GetComponent<GridCell>());
            }
        }
    }
    public void ShowGrid()
    {
        //Vector3 floorSize = GameManager.Instance.GetManager<AquariumManager>().FloorSize;
        CreateGrids();
        Vector3 floorSize = GameManager.Instance.GetManager<AquariumNumericalManager>().FloorSize;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                grids[i * (int)height+j].gameObject.SetActive(true);
                grids[i * (int)height + j].transform.localPosition = new Vector3(i * _distance + _distance/2, 0, j * _distance + _distance/2);

            }
        }
        transform.position = new Vector3(-floorSize.x * 5f, 0, -floorSize.z * 5f);
    }
}
