using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

// for functions to be shared, they need to be virtual, abstract, or override

public abstract class Room : MonoBehaviour
{
    // Controls the screen for the Galley
    public GameManager gameManager;
    public TextMeshProUGUI crewGalleyTxt;
    public Button addCrewBtn;
    public Button subCrewBtn;
    public int crewNeeded;
    public int crew;
    public int maxCrew;

    public TextMeshProUGUI crewCounter;

    public GameObject CrewMember;

    public Transform SpawnLocation;

    private GameObject OtherObject;

    public void updateCrewTxt() {
        crewCounter.text = crew.ToString();
    }

    public void addCrew()
    {
        crew++;
        updateCrewTxt();
    }

    public void subCrew()
    {
        crew--;
        if (crew < 0) crew = 0;
        updateCrewTxt();
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
    // remove all crew at turn end
    public virtual void RemoveAllCrew()
    {
        crew = 0;
        updateCrewTxt();
    }

    // getters
    public virtual int GetCrew()
    {
        return crew;
    }

    public virtual int GetMaxCrew()
    {
        return maxCrew;
    }

    public virtual int GetCrewNeeded()
    {
        return crewNeeded;
    }

    public virtual GameManager GetGameManager()
    {
        return gameManager;
    }


}
