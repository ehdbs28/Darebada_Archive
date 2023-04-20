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
        //1번방법 : Destroy(gameObject);
        //2번방법 : gameObject.SetActive(false);
        //3번방법: GetComponent<MeshRenderer>().enabled = false; GetComponent<Collider>().enabled = false
        //하나 선택 및 3번일 경우 재생성 여부 정해야함.
        
    }
    IEnumerator Refresh()
    {
        yield return new WaitForSeconds(delay);
        blinkStarted= false;
        //3번의 역순
    } 
}