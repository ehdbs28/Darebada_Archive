using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Curl : MonoBehaviour
{

    public Transform _Front;
    public Transform _Mask;
    public Transform _GradOutter;
    public Vector3 _Pos = new Vector3(-240.0f, -470.0f, 0.0f) * 0.01f;

    void LateUpdate()
    {
        //포지션 잡기
        transform.position = _Pos;
        transform.eulerAngles = Vector3.zero;
        //끄트머리부분의 각도
        Vector3 pos = _Front.localPosition;
        float theta = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        //0~90이 아니면 리턴
        if (theta <= 0.0f || theta >= 90.0f) return;

        //반시계로 90도 회전 후 상하반전 후 2배
        float deg = -(90.0f - theta) * 2.0f;
        _Front.eulerAngles = new Vector3(0.0f, 0.0f, deg);
        //각도 설정, 즉 방향이 바뀜

        _Mask.position = (transform.position + _Front.position) * 0.5f;
        //보이는 부분 = 꼭지점과 전 꼭지점의 사이
        _Mask.eulerAngles = new Vector3(0.0f, 0.0f, deg * 0.5f);
        //deg/2 = 대각선 방향
        _GradOutter.position = _Mask.position;

        _GradOutter.eulerAngles = new Vector3(0.0f, 0.0f, deg * 0.5f + 90.0f);

        transform.position = _Pos;
        transform.eulerAngles = Vector3.zero;
    }
}
