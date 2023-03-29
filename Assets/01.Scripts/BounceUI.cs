using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceUI : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(h, 0, v) * Time.deltaTime * 3;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject && collision.gameObject.tag != "Ground")
        {
            StartCoroutine(BounceObj(collision.gameObject));
            rb.AddForce(collision.contacts[0].normal * 10, ForceMode.Impulse);
        }
    }

    IEnumerator BounceObj(GameObject obj)
    {
        obj.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        obj.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }
}
