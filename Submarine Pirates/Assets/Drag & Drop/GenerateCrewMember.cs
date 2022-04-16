using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GenerateCrewMember : MonoBehaviour
{
    public int numofCrew = 10;

    public GameObject CrewMemberPrefab;

    private Camera mainCamera;
    private void Awake() {
        mainCamera = Camera.main;
    }

    private void CreateCrewMember() {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        // 2D object
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
        if (hit2D.collider != null &&(hit2D.collider.gameObject.CompareTag("Storage") || hit2D.collider.
                gameObject.layer == LayerMask.NameToLayer("Storage"))) 
        {
            numofCrew = numofCrew - 1;
            Instantiate(CrewMemberPrefab, transform.position, Quaternion.identity);   
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (numofCrew > 0)
            {
                CreateCrewMember();
            }
        }



    }
}
