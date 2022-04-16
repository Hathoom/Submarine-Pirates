using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Galley : MonoBehaviour
{
    // Controls the screen for the Galley
    public GameManager gameManager;
    public TextMeshProUGUI crewGalleyTxt;
    public Button addCrewBtn;
    public Button subCrewBtn;
    public int crewNeeded;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.crewGalley < crewNeeded)
        {
            // Shows that you will leave crew hungry
        }

        else
        {
            // Crew will be fine
        }

        crewGalleyTxt.text = "Crew On Galley: " + gameManager.crewGalley;
    }

    public void addCrew()
    {
        gameManager.crewGalley++;
    }

    public void subCrew()
    {
        gameManager.crewGalley--;
    }
}
