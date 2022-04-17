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
    public int crewLimit;
    public int crew;
    public int sickCrew;

    public bool atCrewLimit = false;
    public int crewNeeded;

    public TextMeshProUGUI crewCounter;

    public GameObject CrewMember;
    public GameObject SickCrewMember;

    public Transform SpawnLocation;

    public Transform SickSpawnLocation;

    private GameObject OtherObject;

    public void updateCrewTxt() {
        crewCounter.text = crew.ToString();
    }

    public void addCrew()
    {
        crew++;

        if(sickCrew + crew == crewLimit)
        {
            atCrewLimit = true;   
        }
        updateCrewTxt();
    }

    public void subCrew()
    {
        crew--;
        if (crew < 0) crew = 0;
        updateCrewTxt();
    }

    public void addSickCrew()
    {
        sickCrew++;

        if(sickCrew + crew == crewLimit)
        {
            atCrewLimit = true;   
        }
        updateCrewTxt();
    }

    public void subSickCrew()
    {
        sickCrew--;
        updateCrewTxt();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        OtherObject = col.gameObject;
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //take in healthy crew
        if (OtherObject.tag == "Draggable" && OtherObject.layer == 9)
        {
            //can't take in more crew
            if(atCrewLimit)
            {
                Debug.Log("Room is tighter than a clown car");
                OtherObject.transform.position = SpawnLocation.position;
            }
            else
            {
                Destroy(OtherObject);
                addCrew();
            }
        }
        //take in sick crew
        else if (OtherObject.tag == "Draggable" && OtherObject.layer == 8)
        {
            //can't take in more crew
            if(atCrewLimit)
            {
                Debug.Log("Room is tighter than a clown car");
                OtherObject.transform.position = SickSpawnLocation.position;
            }
            else
            {
                Destroy(OtherObject);
                addSickCrew();
            }
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
                atCrewLimit = false;
                subCrew();
                Instantiate(CrewMember, SpawnLocation.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("Room is empty!");
            }
        }
        if (Input.GetKeyDown("q"))
        {
            if (sickCrew > 0)
            {
                atCrewLimit = false;
                subSickCrew();
                Instantiate(SickCrewMember, SickSpawnLocation.position, Quaternion.identity);
            }
        }

    }
    // remove all crew at turn end
    public virtual void RemoveAllCrew()
    {
        crew = 0;
        sickCrew = 0;
        atCrewLimit = false;
        updateCrewTxt();
    }

    // Called at the end of each turn
    public virtual void endTurn() {
        //RemoveAllCrew();
    }

    // getters
    public virtual int GetCrew()
    {
        return crew;
    }

    public virtual int GetCrewLimit()
    {
        return crewLimit;
    }

    public virtual int GetCrewNeeded()
    {
        return crewNeeded;
    }

    public virtual int GetSickCrew()
    {
        return sickCrew;
    }

    public virtual GameManager GetGameManager()
    {
        return gameManager;
    }
}
