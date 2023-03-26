using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePopUp : MonoBehaviour
{
    [SerializeField] private LayerMask _buttonLayers;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(pos, Vector3.forward, out hit, 20f, _buttonLayers))
            {
                hit.collider.GetComponent<CloseButton>().CloseWindow();
            }

        }
    }
}
