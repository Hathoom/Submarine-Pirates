using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewBase : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject CrewMember;

    public Transform SpawnLocation;

    private GameObject OtherObject;

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

        OtherObject = col.gameObject;
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (OtherObject.tag == "Draggable")
        {
            Destroy(OtherObject);
            gameManager.crew = gameManager.crew + 1;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        OtherObject = null;
    }

    void OnMouseDown()
    {
        if (gameManager.crew > 0)
        {
            gameManager.crew = gameManager.crew - 1;
            Instantiate(CrewMember, SpawnLocation.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("CrewBase is empty!");
        }
    }
}
