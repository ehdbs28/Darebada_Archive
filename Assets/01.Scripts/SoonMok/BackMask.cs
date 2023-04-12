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
        switch(front.back)
        {
            case Fronts.BackPivot.x:
            if (front.theta < front.min || front.theta > front.max) return;
                break;
            case Fronts.BackPivot.y:
                break;
            case Fronts.BackPivot.z:
                if (front.theta < -180 || front.theta > -90) return;

                break;
        }

        Vector3 pos = front.transform.localPosition /2;
        transform.localPosition = pos;
        float deg = -(angle - front.theta);
        //transform.eulerAngles = new Vector3(0, 0, deg);
        transform.localRotation = Quaternion.Euler(0, 0, deg);
    }

    
}
