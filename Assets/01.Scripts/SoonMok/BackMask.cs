using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class BackMask : MonoBehaviour
{
    [SerializeField] Fronts front;
    [SerializeField] float angle;
    private void LateUpdate()
    {
        if (front.theta < front.min || front.theta > front.max) return;

        Vector3 pos = front.transform.localPosition /2;
        transform.localPosition = pos;
        float deg = -(angle - front.theta);
        transform.eulerAngles = new Vector3(0, 0, deg);
    }

    
}
