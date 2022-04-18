using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Galley : Room
{
    public override void endTurn() {
        // alter what needs to be altered
        gameManager.foodInc(crew * 5);
        gameManager.foodInc(sickCrew * 3);
        base.endTurn();
    }

    public override void SetCrewNeeded() {
        crewNeeded = (gameManager.usableCrew + 4) / 5;
    }
}
