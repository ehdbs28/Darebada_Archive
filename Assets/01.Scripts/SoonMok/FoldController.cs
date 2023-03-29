using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FoldController : MonoBehaviour, IsClickObj
{
    public int foldCount;
    public GameObject foldParent;
    public Fold[] folds;
    public float ySize;
    public bool isOpened;

    private void Awake()
    {
        folds = FindObjectsOfType<Fold>();
        ySize = transform.parent.localScale.y;
        foldCount=folds.Length; 
    }
    private void Update()
    {
    }
    public void OpenFolds(Vector3 dir)
    {
        isOpened = !isOpened;
        for(int i = 0; i < foldCount; i++)
        {
            folds[i].isUp = dir == Vector3.up ? true: false;
            folds[i].target = dir * ySize * (i+1);
            folds[i].isMoving = true;
        }
    }

    public void OnClick()
    {

        if (isOpened)
        {
            OpenFolds(Vector3.zero);
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            OpenFolds(Vector3.up);
            
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void OnDrag()
    {
    }

    public void OnDragEnd()
    {
    }
}
