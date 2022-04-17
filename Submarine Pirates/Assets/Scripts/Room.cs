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

    public void addCrew()
    {
        crew++;
    }

    public void subCrew()
    {
        crew--;
        if (crew < 0) crew = 0;
    }
}
