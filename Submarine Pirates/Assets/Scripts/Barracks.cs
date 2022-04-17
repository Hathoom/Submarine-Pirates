using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Room
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // override the RemoveAllCrew to make changes when turn ends
    public override void RemoveAllCrew()
    {
        // grab necessary variables.
        GameManager gameManager = base.GetGameManager();
        int crew = base.GetCrew();

        // alter what needs to be altered
        gameManager.happinessInc(crew * 5);

        //Call the original RemoveAllCrew to reset the zone
        base.RemoveAllCrew();

    }
}
