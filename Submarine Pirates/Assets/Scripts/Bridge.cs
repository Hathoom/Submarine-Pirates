using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Bridge : Room
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (crew < crewNeeded)
        {
            // Shows that you will leave crew hungry
        }

        else
        {
            // Crew will be fine
        }

        crewGalleyTxt.text = "Crew On Bridge: " + crew;
    }
}
