using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class OceanFishController : ModuleController
{
    private FishDataUnit _dataUnit;
    public FishDataUnit DataUnit => _dataUnit;

    private FishActionData _actionData;
    public FishActionData ActionData => _actionData;

    private Transform _modelTrm;
    
    public bool CompleteSetting { get; set; }
    
    protected override void Awake()
    {
        base.Awake();
        _actionData = GetComponent<FishActionData>();
        _modelTrm = transform.Find("Model");
    }

    protected override void Update()
    {
        if (!CompleteSetting)
            return;
        
        base.Update();
    }

    protected override void FixedUpdate()
    {
        if (!CompleteSetting)
            return;
        
        base.FixedUpdate();
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public void SetUp(FishDataUnit data, BoxCollider bound)
    {
        _dataUnit = data;
        
        // Should Check Visual

        _actionData.Lenght = Random.Range(data.MinLenght, data.MaxLenght);
        float normalizedLenght = (_actionData.Lenght - data.MinLenght) / (data.MaxLenght - data.MinLenght);
        _actionData.Weight = normalizedLenght * (data.MaxWeight - data.MinWeight) + data.MinWeight;

        transform.localScale = Vector3.one * normalizedLenght;
        
        GetModule<FishMovementModule>().SetBound(bound);
        CompleteSetting = true;
    }

    public void GetoutBait()
    {
        _actionData.IsCatch = false;
        _actionData.IsSensed = false;
        _actionData.BaitTrm = null;
    }

    public void SuccessCatching(Vector3 end, float height, float rotateSpeedX, float rotateSpeedZ)
    {
        float rotX = 0f;
        float rotZ = 0f;
        int rotDir = 1;
        
        transform.DOJump(end, height, 1, 1).SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                if (Mathf.Abs(rotX) >= 10f)
                {
                    rotDir *= -1;
                    rotX = Mathf.Clamp(rotX, -29f, 29f);
                }

                rotZ += rotateSpeedZ * Time.deltaTime;
                rotX += rotateSpeedX * rotDir * Time.deltaTime;

                transform.rotation = Quaternion.AngleAxis(rotX, Vector3.right);
                _modelTrm.localRotation = Quaternion.AngleAxis(rotZ, Vector3.forward);
            })
            .OnComplete(() =>
            {
                GameManager.Instance.GetManager<PoolManager>().Push(this);
            });       
    }
}
