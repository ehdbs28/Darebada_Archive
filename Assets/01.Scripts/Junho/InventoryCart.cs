using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCart : MonoBehaviour
{
    public List<GameObject> innerObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            innerObj.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }

    public void OpenCart()
    {
        gameObject.SetActive(true);
    }

    public void CloseCart()
    {
        for (int i = 0; i < innerObj.Count; i++)
        {
            Instantiate(innerObj[i]).gameObject.SetActive(true);
        }

        innerObj.Clear();
        gameObject.SetActive(false);

    }


}
