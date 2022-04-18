using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Room
{
    public override void endTurn()
    {
        // alter what needs to be altered
        gameManager.happinessInc(crew * 5);
        gameManager.happinessInc(sickCrew * 3);

        base.endTurn();
    }
}
