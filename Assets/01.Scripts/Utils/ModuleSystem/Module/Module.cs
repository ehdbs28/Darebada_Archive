using UnityEngine;

public interface Module
{
    public void SetUp(Transform rootTrm);
    public void UpdateModule();
    public void FixedUpdateModule();
}