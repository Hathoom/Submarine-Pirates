using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maintenance : Room
{
    public override void endTurn()
    {
        // alter what needs to be altered
        gameManager.damageInc(-crew * 2);
        gameManager.damageInc(-sickCrew);

        base.endTurn();
    }

    public override void SetCrewNeeded() {
        crewNeeded = gameManager.damage / 2;
    }
}
