using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterDefend : Encounter
{
    public int weaponsNeeded;
    public int damagePerCrew;
    public int healthPerCrew;

    public EncounterDefend(int weaponsNeeded, int damagePerCrew, int healthPerCrew) {
        // 0 - Surface Merchant
        // 1 - Fishing Vessal
        // 2 - ??
        // 3 - Scrappers
        // 4 - Davey Jones
        this.weaponsNeeded = weaponsNeeded;
        this.damagePerCrew = damagePerCrew;
        this.healthPerCrew = healthPerCrew;
    }

    public override void executeEncounter() {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gameManager.weaponPow == weaponsNeeded) {
            // Good
        } else if (gameManager.weaponPow < weaponsNeeded) {
            gameManager.damageInc((weaponsNeeded - gameManager.weaponPow) * damagePerCrew);
            gameManager.healthInc(-(weaponsNeeded - gameManager.weaponPow) * healthPerCrew);
        }

        gameManager.startTurn();
    }
}