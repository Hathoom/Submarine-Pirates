using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select3DObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Debug.Log("3D hit: " + hitInfo.collider.gameObject.name);
            }

        }
    }
}
