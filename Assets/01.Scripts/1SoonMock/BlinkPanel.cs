using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkPanel : MonoBehaviour
{
    public float delay;
    public string tag;
    public bool blinkStarted;
    private void OnCollisionEnter(Collision collision)
    {
        if(!blinkStarted)
        { StartCoroutine(Blink()); }
    }
    IEnumerator Blink()
    {
        blinkStarted= true;
        yield return new WaitForSeconds(delay);
        //1����� : Destroy(gameObject);
        //2����� : gameObject.SetActive(false);
        //3�����: GetComponent<MeshRenderer>().enabled = false; GetComponent<Collider>().enabled = false
        //�ϳ� ���� �� 3���� ��� ����� ���� ���ؾ���.
        
    }
    IEnumerator Refresh()
    {
        yield return new WaitForSeconds(delay);
        blinkStarted= false;
        //3���� ����
    } 
}