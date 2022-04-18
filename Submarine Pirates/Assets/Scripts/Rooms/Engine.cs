using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Engine : Room
{
    public override void endTurn() {
        // alter what needs to be altered
        //gameManager.depthInc();

        //Debug.Log((GetCrew() * 10) + (GetSickCrew() * 5));

        int totalDepth = (GetCrew() * 10) + (GetSickCrew() * 5);

        //Debug.Log("totalDepth: " + totalDepth);

        gameManager.depthInc(totalDepth);

        gameManager.fuelInc(-1);

        base.endTurn();
    }
    
}
