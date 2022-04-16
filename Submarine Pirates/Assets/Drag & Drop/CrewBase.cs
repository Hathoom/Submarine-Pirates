using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewBase : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
       
    }  

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        if (col.gameObject.name == "Crewmember")
        {
            gameManager.crew = gameManager.crew + 1;
            Destroy(col.gameObject);
        }
    }
}
