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
                    print("��ħ");
                    MiniGameManager.Instance.radius += 100;
                    for (int j = 0; j < MiniGameManager.Instance.hitPoint.Count; j++)
                    {
                        MiniGameManager.Instance.hitPoint[j].transform.localScale /= 2;
                    }

                    // ���� ���⼭ ����� ��ġ �� �ø��� �ɵ�
                    // ����Ʈ ���ֱ�
                }
                else
                {
                    // ����� ��ġ ���߱�

                }
            }
        }
        // �Ÿ��� 0�̸� �κ��丮�� �� ����� �־����
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
