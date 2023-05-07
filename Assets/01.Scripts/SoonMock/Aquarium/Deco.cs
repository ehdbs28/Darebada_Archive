using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deco : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    public Vector3 pos;
    [SerializeField] private TransformsSO posList;
    public int id;

    public void SetId(int id)
    {
        this.id = id;
        this.pos = posList.trs[id];
        //스프라이트 또는 모델링 변경해야함
    }
}
