using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickCrewBase : MonoBehaviour
{
public GameManager gameManager;

    public GameObject SickCrewMember;

    public Transform SpawnLocation;

    private GameObject OtherObject;


    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        OtherObject = col.gameObject;
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        // only take in Sick Crew Members
        if (OtherObject.tag == "Draggable" && OtherObject.layer == 8)
        {
            Destroy(OtherObject);
            gameManager.crewSick = gameManager.crewSick + 1;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        OtherObject = null;
    }

    void OnMouseDown()
    {
        if (gameManager.crewSick > 0)
        {
            gameManager.crewSick = gameManager.crewSick - 1;
            Instantiate(SickCrewMember, SpawnLocation.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("SickCrewBase is empty!");
        }
    }
}
