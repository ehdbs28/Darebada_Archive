using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameBall : MonoBehaviour
{
    public float range;

    private Vector3 retVector;
    private float degree;

    private void Update()
    {
        CrossBall();
    }

    private void FixedUpdate()
    {
        SpinBall();
    }

    private void CrossBall()
    {
        range = MiniGameManager.Instance.SetRange();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < MiniGameManager.Instance.hitPoint.Count; i++)
            {
                if (MiniGameManager.Instance.hitPoint[i].transform.position.x + range > retVector.x
                    && MiniGameManager.Instance.hitPoint[i].transform.position.x - range < retVector.x)
                {
                    print("겹침");
                    MiniGameManager.Instance.radius += 100;
                    for (int j = 0; j < MiniGameManager.Instance.hitPoint.Count; j++)
                    {
                        MiniGameManager.Instance.hitPoint[j].transform.localScale /= 2;
                    }

                    // 대충 여기서 물고기 위치 값 올리면 될듯
                    // 포인트 없애기
                }
                else
                {
                    // 물고기 위치 낮추기

                }
            }
        }
        // 거리가 0이면 인벤토리로 그 물고기 넣어버림
    }

    private void SpinBall()
    {
        retVector = transform.position;
        degree += MiniGameManager.Instance.radius / 6;
        float radian = degree * Mathf.PI / 180;

        retVector.x += MiniGameManager.Instance.radius * Mathf.Cos(radian);
        retVector.y += MiniGameManager.Instance.radius * Mathf.Sin(radian);

        transform.position = retVector;
    }
}
