using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public abstract class Room : MonoBehaviour
{
    // Controls the screen for the Galley
    public GameManager gameManager;
    public TextMeshProUGUI crewGalleyTxt;
    public Button addCrewBtn;
    public Button subCrewBtn;
    public int crewNeeded;
    public int crew;

    public GameObject CrewMember;

    public Transform SpawnLocation;

    private GameObject OtherObject;

    public void addCrew()
    {
        crew++;
    }

    public void subCrew()
    {
        crew--;
        if (crew < 0) crew = 0;
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
            addCrew();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        OtherObject = null;
    }

    // remove a crewmember from location
    void OnMouseOver()
    {
        //on right click
        if (Input.GetMouseButtonDown(1))
        {
            if (crew > 0)
            {
                subCrew();
                Instantiate(CrewMember, SpawnLocation.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("Room is empty!");
            }
        }

    }

}
