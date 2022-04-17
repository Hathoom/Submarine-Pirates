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

    }

    public override void RemoveAllCrew()
    {
        // grab necessary variables.
        GameManager gameManager = base.GetGameManager();
        int crew = base.GetCrew();

        int level = gameManager.getLevel();
        int damage = gameManager.getDamage();

        // alter what needs to be altered

        int crewNeeded =  level - 1;

        if (2 < damage / 5)
        {
            crewNeeded += 2;
        }
        else
        {
            crewNeeded += (damage / 5);
        }
        // level - 1 + (hull / 5)

        //Call the original RemoveAllCrew to reset the zone
        base.RemoveAllCrew();

    }
}
