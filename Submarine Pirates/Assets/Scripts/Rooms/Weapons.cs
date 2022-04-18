using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : Room
{
    public int weaponPow = 0;

    public override void endTurn()
    {

        // alter what needs to be altered
        weaponPow = weaponPow + crew;
        if (sickCrew > 0)
        {
            for (int i = 0; i < crew;  i++)
            {
                if (Random.Range(0, 101) >= 50)
                {
                    weaponPow++;
                }
            }
        }

        gameManager.weaponPowInc(weaponPow);
        weaponPow = 0;
        
        base.endTurn();
    }

    void SetCrewNeeded() {
        crewNeeded = gameManager.level;
    }
}
