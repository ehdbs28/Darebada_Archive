using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceUI : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb;
        rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null && rb.CompareTag("Player"))
        {
            StartCoroutine(BounceObj(gameObject));
            rb.AddForce(collision.contacts[0].normal * -10, ForceMode.Impulse);
        }
    }

    IEnumerator BounceObj(GameObject obj)
    {
        obj.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        obj.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }
}
